using MongoDB.Bson.Serialization.Attributes;

public class Stock
{
    [BsonId]
    public string StockID {  get; set; }
    public string OutletID { get; set; }
    public string ProductID { get; set; }
    public int StockLevel { get; set; }
}
