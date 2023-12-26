using MongoDB.Bson.Serialization.Attributes;

namespace DDServices.Models
{
    public class Outlet
    {
        [BsonId]
        public string OutletID { get; set; }
        public string OutletName { get; set; }
        public string Location { get; set; }
    }
}
