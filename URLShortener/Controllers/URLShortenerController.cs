using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using URLShortener.Dtos;
using UrlShortener.Services;

namespace UrlShortener.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UrlShortenerController : ControllerBase
    {
        private readonly UrlShortenerService _shortenerService;

        public UrlShortenerController(UrlShortenerService service)
        {
            _shortenerService = service;
        }

        [HttpPost]
        public async Task<ActionResult<UrlShortenResponse>> PostAsync([FromBody] UrlShortenRequest url)
        {
            try
            {
                var result = await _shortenerService.ShortenAsync(url.LongUrl);
                var response = new UrlShortenResponse()
                {
                    LongUrl = url.LongUrl,
                    ShortUrl = $"{this.Request.Scheme}://{this.Request.Host}/{result}"
                };

                return new JsonResult(response);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("/{shortUrl}")]
        public async Task<IActionResult> GetAsync(string shortUrl)
        {
            try
            {
                var longUrl = await _shortenerService.GetLongUrlAsync(shortUrl);
                return RedirectPermanent(longUrl);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
