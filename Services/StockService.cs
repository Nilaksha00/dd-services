using DDServices.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DDServices.Services
{
    public class StockService
    {
        private readonly IMongoCollection<Stock> _stock;

        public StockService(IOptions<MongoDBSettings> options)
        {
            var mongoDBClient = new MongoClient(options.Value.ConnectionURI);

            _stock = mongoDBClient.GetDatabase(options.Value.DatabaseName).GetCollection<Stock>(options.Value.StockCollectionName);
        }

        public async Task<List<Stock>> GetStockListByOutletId(string outletId) =>
            await _stock.Find(s => s.outletID == outletId).ToListAsync();

        public async Task CreateStock(Stock newStock)
        {
            newStock.stockID = Guid.NewGuid().ToString("N");
            await _stock.InsertOneAsync(newStock);
        }

        public async Task UpdateStock(string outletId, string productId, int newStockLevel)
        {
            var filter = Builders<Stock>.Filter.Eq(s => s.outletID, outletId) & Builders<Stock>.Filter.Eq(s => s.productID, productId);
            var update = Builders<Stock>.Update.Set(s => s.quantity, newStockLevel);
            await _stock.UpdateOneAsync(filter, update);
        }

    }
}
