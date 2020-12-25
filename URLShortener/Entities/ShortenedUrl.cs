using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace UrlShortener.Entities
{
    public class ShortenedUrl
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Url { get; set; }

        public string UrlShort { get; set; }
    }
}
