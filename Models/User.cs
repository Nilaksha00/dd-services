using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DDServices.Models
{
    public class User
    {
        [BsonId]
        public string? userID { get; set; }
        public string? email { get; set; }
        public string? password { get; set; }
        public string? name { get; set;}
        public string? role { get; set; }
        public Boolean? isActive { get; set;}
    }
}
