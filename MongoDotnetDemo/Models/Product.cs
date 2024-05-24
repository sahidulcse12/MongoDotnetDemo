using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MongoDotnetDemo.Models
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = default!;
        public string ProductName { get; set; } = default!;

        [BsonRepresentation(BsonType.ObjectId)]
        public string CategoryId { get; set; } = default!;

        // This property, will not store in database, if you pass a null value to it, so make sure make it null before passing to db
        [BsonIgnoreIfNull]
        public string CategoryName { get; set; } = default!;
    }
}
