using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace URLShortener.Controllers
{
    [ApiController]
    [Route("")]
    public class URLShortenerController : ControllerBase
    {
        [HttpPost]
        [Route("{url}")]
        public string Post(string url)
        {
            return null;
        }
    }
}
