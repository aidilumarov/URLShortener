using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.WebUtilities;
using MongoDB.Driver;
using UrlShortener.Entities;
using URLShortener.Lib;

namespace UrlShortener.Services
{
    public class UrlShortenerService
    {
        #region Fields

        private readonly IMongoCollection<ShortenedUrl> _shortenedUrls;
        private readonly Random _randomizer;

        #endregion

        #region Constructor

        public UrlShortenerService(IShortenedUrlStoreDbSettings dbSettings, Random randomizer)
        {
            _randomizer = randomizer;
            
            var client = new MongoClient(dbSettings.ConnectionString);
            var database = client.GetDatabase(dbSettings.DatabaseName);

            _shortenedUrls = database.GetCollection<ShortenedUrl>(dbSettings.ShortenedUrlCollectionName);
        }

        #endregion

        #region PublicMethods

        /// <summary>
        /// Shortens the provided providedUrl and returns the shortened version
        /// </summary>
        /// <param name="providedUrl"></param>
        /// <exception cref="UriFormatException">URL is invalid or does not exist on the internet</exception>
        /// <returns>Shortened string</returns>
        public async Task<string> ShortenAsync(string providedUrl)
        {
            // Check if providedUrl is valid
            if (!IsValidUrl(providedUrl))
            {
                throw new UriFormatException("URL is not valid.");
            }

            // Check if the same record exists in db
            var result = await _shortenedUrls.FindAsync<ShortenedUrl>(x => x.Url == providedUrl);
            var shortUrl = result.FirstOrDefault();
            if (shortUrl != null)
            {
                return shortUrl.UrlShort;
            }

            int randomId = 0;
            while (true)
            {
                randomId = _randomizer.Next(1, int.MaxValue);
                var duplicate = await GetAsync(randomId);
                if (duplicate == null)
                {
                    break;
                }
            }

            if (randomId == 0) return "Database full";
            
            var newShortUrl = new ShortenedUrl()
            {
                Id = randomId,
                Url = providedUrl,
                UrlShort = LinkShortener.Encode(randomId)
            };

            await CreateAsync(newShortUrl);
            
            return newShortUrl.UrlShort;
        }

        /// <summary>
        /// Converts short links back to long URLs
        /// </summary>
        /// <param name="shortUrl"></param>
        /// <returns>Long URL</returns>
        /// <exception cref="ArgumentException">Short URL is too long or does not exist</exception>
        public async Task<string> GetLongUrlAsync(string shortUrl)
        {
            if (shortUrl.Length > 20)
            {
                throw new ArgumentException("Short URL you gave is not that short!");
            }
            
            var decoded = LinkShortener.Decode(shortUrl);
            var record = await GetAsync(decoded);

            if (record == null)
            {
                throw new ArgumentException("Invalid short URL.");
            }

            return record.Url;
        }
        
        #endregion

        #region DatabaseQueries

        private async Task<ShortenedUrl> CreateAsync(ShortenedUrl shortenedUrl)
        {
            await _shortenedUrls.InsertOneAsync(shortenedUrl);
            return shortenedUrl;
        }

        private async Task<ShortenedUrl> GetAsync(int id)
        {
            var result = await _shortenedUrls
                .FindAsync<ShortenedUrl>(x => x.Id == id);
            return result.FirstOrDefault();
        }

        #endregion

        #region UrlShortener

        private static bool IsValidUrl(string url)
        {
            try
            {
                var uri = new Uri(url);
                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion
    }
}
