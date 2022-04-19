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
        private readonly ApplicationDbContext _dbContext;
        private readonly IRepository<ShortUrl> _shortUrlRepository;

        public ShortUrlsController(ApplicationDbContext dbContext, IRepository<ShortUrl> shortUrlRepository)
        {
            _dbContext = dbContext;
            _shortUrlRepository = shortUrlRepository;
        }

        // GET: api/ShortUrls
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShortUrl>>> GetShortUrls()
        {
            return await _dbContext.ShortUrls.ToListAsync();
        }

        [HttpGet]
        [Route("/api/ShortUrlsV2")]
        public List<ShortUrl> GetShortUrlsV2()
        {
            return _shortUrlRepository.GetAll().ToList();
        }

        // GET: api/ShortUrls/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ShortUrl>> GetShortUrl(long id)
        {
            var shortUrl = await _dbContext.ShortUrls.FindAsync(id);

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

            _dbContext.Entry(shortUrl).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShortUrlExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ShortUrls
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ShortUrl>> PostShortUrl(ShortUrl shortUrl)
        {
            _dbContext.ShortUrls.Add(shortUrl);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetShortUrl), new { id = shortUrl.Id }, shortUrl);
        }

        // DELETE: api/ShortUrls/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShortUrl(long id)
        {
            var shortUrl = await _dbContext.ShortUrls.FindAsync(id);
            if (shortUrl == null)
            {
                return NotFound();
            }

            _dbContext.ShortUrls.Remove(shortUrl);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        private bool ShortUrlExists(Guid id)
        {
            return _dbContext.ShortUrls.Any(e => e.Id == id);
        }
    }
}
