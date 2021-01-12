using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.IdGenerators;

namespace UrlShortener.Entities
{
    public class MongoDbConfig : IMongoDbConfig
    {
        public string Database { get; set; }
        public string ShortUrlCollection { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string User { get; set; }
        public string Password { get; set; }

        public string ConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(User) ||
                    string.IsNullOrEmpty(Password))
                {
                    return $@"mongodb://{Host}:{Port}";
                }

                return $@"mongodb://{User}:{Password}@{Host}:{Port}";
            }
        }
    }

    public interface IMongoDbConfig
    {
        string Database { get; set; }
        string ShortUrlCollection { get; set; }
        string Host { get; set; }
        int Port { get; set; }
        string User { get; set; }
        string Password { get; set; }
        string ConnectionString { get; }
    }
}
