using MongoDB.Bson.Serialization.Attributes;

namespace DDServices.Models
{
    public class Preorder
    {
        [BsonId]
        public string PreorderID { get; set; }
        public string ProductID { get; set; }
        public int Quantity { get; set; }
        public DateTime DatePlaced { get; set; }
        public bool IsLocked { get; set; }
        // Add other preorder-related properties as needed
    }
}
