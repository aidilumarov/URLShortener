using System;
using Newtonsoft.Json;
using UrlShortener.Entities;
using System.IO;
using Newtonsoft.Json.Linq;

namespace URLShortener.Tests.Common
{
    public class ApplicationConfigGetter
    {
        /// <summary>
        /// Get config settings
        /// </summary>
        /// <returns>DbSettings or null</returns>
        public static ShortenedUrlStoreDbSettings ParseDbConfigSection()
        {
            try
            {
                var json = File.ReadAllText("appsettings.json");
                var parsedJson = JObject.Parse(json);
                var dbConfigSection = parsedJson["ShortenedUrlStoreDbSettings"].ToString();
                return JsonConvert.DeserializeObject<ShortenedUrlStoreDbSettings>(dbConfigSection);
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}