using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.WebUtilities;
using MongoDB.Driver;
using UrlShortener.Entities;

namespace UrlShortener.Services
{
    public class UrlShortenerService
    {
        #region Fields

        private readonly IMongoCollection<ShortenedUrl> _shortenedUrls;

        #endregion

        #region Constructor

        public UrlShortenerService(IShortenedUrlStoreDbSettings dbSettings)
        {
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
        /// <returns></returns>
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

            // Create new record and get id
            var newShortUrl = new ShortenedUrl()
            {
                Url = providedUrl
            };

            await _shortenedUrls.InsertOneAsync(newShortUrl);

            // Use this id to 
            var shortUrlChunk = GetShortUrlChunk(newShortUrl.Id);
            newShortUrl.UrlShort = shortUrlChunk;

            // Update the record
            var filter = Builders<ShortenedUrl>.Filter.Eq(nameof(newShortUrl.Id), newShortUrl.Id);
            var update = Builders<ShortenedUrl>.Update.Set(nameof(newShortUrl.UrlShort), newShortUrl.UrlShort);
            await _shortenedUrls.UpdateOneAsync(filter, update);

            return shortUrlChunk;
        }

        #endregion

        #region DatabaseQueries

        private async Task<ShortenedUrl> CreateAsync(ShortenedUrl shortenedUrl)
        {
            await _shortenedUrls.InsertOneAsync(shortenedUrl);
            return shortenedUrl;
        }

        private async Task<ShortenedUrl> GetAsync(string id)
        {
            var result = await _shortenedUrls
                .FindAsync<ShortenedUrl>(x => x.Id == id);
            return result.FirstOrDefault();
        }

        #endregion

        #region UrlShortener

        private string GetShortUrlChunk(string id)
        {
            return WebEncoders.Base64UrlEncode(Encoding.ASCII.GetBytes(id));
        }

        private string GetIdFromShortUrlChunk(string urlChunk)
        {
            return Encoding.ASCII.GetString(WebEncoders.Base64UrlDecode(urlChunk));
        }

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
