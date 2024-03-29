﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Another_URL_Shortener.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Another_URL_Shortener.Models;
using Another_URL_Shortener.Repositories;
using Another_URL_Shortener.Requests;
using Another_URL_Shortener.Responses;
using Another_URL_Shortener.Services;

namespace Another_URL_Shortener.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShortUrlsController : ControllerBase
    {
        readonly IServiceHandler<BaseRequest> _serviceHandler;

        public ShortUrlsController(IServiceHandler<BaseRequest> serviceHandler)
        {
            _serviceHandler = serviceHandler;
        }

        // GET: api/ShortUrls
        [HttpGet]
        //[Route("/api/ShortUrls")]
        public async Task<IActionResult> GetShortUrls()
        {
            var response = await _serviceHandler.HandleRequest(new GetShortUrlsRequest());
            return Ok(response);
        }

        // GET: api/ShortUrls/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ShortUrl>> GetShortUrl(Guid id)
        {
            var response = (GetShortUrlsResponse)await _serviceHandler.HandleRequest(new GetShortUrlsRequest() { Id = id });
            if (!response.ShortUrls.Any())
            {
                return NotFound();
            }

            return Ok(response);
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

            var response = (GetShortUrlsResponse)await _serviceHandler.HandleRequest(new PostShortUrlRequest()
            {
                ShortUrl = shortUrl,
                IsModified = true
            });

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response.ShortUrls.FirstOrDefault());
        }

        // POST: api/ShortUrls
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ShortUrl>> PostShortUrl(ShortUrl shortUrl)
        {
            var response = await _serviceHandler.HandleRequest(new PostShortUrlRequest()
            {
                ShortUrl = shortUrl
            });

            if (response is ExceptionResponse { Message: { } } exceptionResponse)
            {
                return Problem(exceptionResponse.Message);
            }

            return CreatedAtAction(nameof(GetShortUrl), new { id = shortUrl.Id }, shortUrl);
        }

        // DELETE: api/ShortUrls/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShortUrl(Guid id)
        {
            var response = await _serviceHandler.HandleRequest(new DeleteShortUrlsRequest(){Id = id});

            if (response == null)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
