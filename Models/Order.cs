using MongoDB.Bson.Serialization.Attributes;

public class Order
{
    [BsonId]
    public string orderID { get; set; }
    public string productID { get; set; }
    public string quantity { get; set; }
    public string total { get; set; }

    public string outletID { get; set; }
    public string orderStatus { get; set; }
    public DateTime orderDate { get; set; }
}
