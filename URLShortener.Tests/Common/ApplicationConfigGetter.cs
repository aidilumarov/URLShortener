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
        public static MongoDbConfig ParseDbConfigSection()
        {
            try
            {
                var json = File.ReadAllText("appsettings.json");
                var parsedJson = JObject.Parse(json);
                var dbConfigSection = parsedJson["MongoDbConfig"].ToString();
                return JsonConvert.DeserializeObject<MongoDbConfig>(dbConfigSection);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}