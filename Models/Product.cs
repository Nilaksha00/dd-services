using MongoDB.Bson.Serialization.Attributes;

namespace DDServices.Models
{
    public class Product
    {
        [BsonId]
        public string? productID { get; set; }
        public string? name { get; set; }
        public string? category { get; set; }
        public string? size { get; set; }
        public string? quantity { get; set; }
        public string? price { get; set; }
        public string? status { get; set; }
    }
}
