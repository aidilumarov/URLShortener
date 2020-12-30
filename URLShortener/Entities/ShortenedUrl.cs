using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace UrlShortener.Entities
{
    public class ShortenedUrl
    {
        [BsonId]
        [BsonRepresentation(BsonType.Int32)]
        public int Id { get; set; }

        public string Url { get; set; }

        public string UrlShort { get; set; }
    }
}
