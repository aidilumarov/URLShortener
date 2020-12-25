using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace URLShortener.Entities
{
    public class ShortenedUrlStoreDbSettings : IShortenedUrlStoreDbSettings
    {
        public string ShortenedUrlCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IShortenedUrlStoreDbSettings
    {
        string ShortenedUrlCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
