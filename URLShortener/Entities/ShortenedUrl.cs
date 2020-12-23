using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace URLShortener.Entities
{
    public class ShortenedUrl
    {
        public int Id { get; set; }

        public string Url { get; set; }

        public string UrlShort { get; set; }
    }
}
