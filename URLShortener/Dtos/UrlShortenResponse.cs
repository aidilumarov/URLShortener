using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace URLShortener.Dtos
{
    public class UrlShortenResponse
    {
        public string LongUrl { get; set; }
        
        public string ShortUrl { get; set; }
    }
}
