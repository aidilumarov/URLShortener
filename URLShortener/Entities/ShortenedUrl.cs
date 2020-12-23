using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace URLShortener.Entities
{
    public class ShortenedUrl
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public int Id { get; set; }

        public string Url { get; set; }

        public string UrlShort { get; set; }
    }
}
