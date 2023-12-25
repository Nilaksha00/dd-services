namespace DDServices
{
    public class MongoDBSettings
    {
        public string ConnectionURI { get; set; } = null!;
        public string DatabaseName { get; set;} = null!;
        public string UserCollectionName { get; set;} = null!;
        public string ProductCollectionName { get; set;} = null!;
        public string OrderCollectionName { get; set; } = null!;

    }
}
