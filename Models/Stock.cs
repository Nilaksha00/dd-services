using MongoDB.Bson.Serialization.Attributes;

public class Stock
{
    [BsonId]
    public string stockID {  get; set; }
    public string outletID { get; set; }
    public string productID { get; set; }
    public string productName { get; set; }
    public string productCategory { get; set; }
    public string productSize { get; set; }
    public string productBuyingPrice { get; set; }
    public string productSellingPrice { get; set; }
    public int quantity { get; set; }
}
