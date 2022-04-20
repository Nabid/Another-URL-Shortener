using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Another_URL_Shortener.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Another_URL_Shortener.Models;
using Another_URL_Shortener.Services;

namespace Another_URL_Shortener.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShortUrlsController : ControllerBase
    {
        private readonly IRepository<ShortUrl> _shortUrlRepository;

        public ShortUrlsController(ApplicationDbContext dbContext, IRepository<ShortUrl> shortUrlRepository)
        {
            _shortUrlRepository = shortUrlRepository;
        }

        // GET: api/ShortUrls
        [HttpGet]
        public async Task<IEnumerable<ShortUrl>> GetShortUrls()
        {
            return await _shortUrlRepository.GetAll();
        }

        [HttpGet]
        [Route("/api/ShortUrlsV2")]
        public Task<List<ShortUrl>> GetShortUrlsV2()
        {
            return _shortUrlRepository.GetAll();
        }

        // GET: api/ShortUrls/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ShortUrl>> GetShortUrl(Guid id)
        {
            var shortUrl = await _shortUrlRepository.Find(x => x.Id == id).FirstOrDefaultAsync();

            if (shortUrl == null)
            {
                return NotFound();
            }

            return shortUrl;
        }

        // PUT: api/ShortUrls/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShortUrl(Guid id, ShortUrl shortUrl)
        {
            if (id != shortUrl.Id)
            {
                return BadRequest();
            }

            _shortUrlRepository.ModifyContextState(shortUrl);

            try
            {
                await _shortUrlRepository.SaveContext();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_shortUrlRepository.Find(x => x.Id == id) == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(shortUrl);
        }

        // POST: api/ShortUrls
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ShortUrl>> PostShortUrl(ShortUrl shortUrl)
        {
            _shortUrlRepository.Add(shortUrl);
            //await _shortUrlRepository.SaveChangesAsync();

            return CreatedAtAction(nameof(GetShortUrl), new { id = shortUrl.Id }, shortUrl);
        }

        // DELETE: api/ShortUrls/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShortUrl(Guid id)
        {
            var shortUrl = await _shortUrlRepository.Find(x => x.Id == id).FirstOrDefaultAsync();
            if (shortUrl == null)
            {
                return NotFound();
            }

            _shortUrlRepository.Delete(shortUrl);
            //await _dbContext.SaveChangesAsync();

            //return NoContent();
            return Ok();
        }
    }
}
