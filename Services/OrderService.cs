using DDServices.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DDServices.Services
{
    public class OrderService
    {
        private readonly IMongoCollection<Order> _order;

        public OrderService(IOptions<MongoDBSettings> options)
        {
            var mongoDBClient = new MongoClient(options.Value.ConnectionURI);

            _order = mongoDBClient.GetDatabase(options.Value.DatabaseName).GetCollection<Order>(options.Value.OrderCollectionName);
        }

        public async Task<List<Order>> GetOrderList() =>
            await _order.Find(_ => true).ToListAsync();

        public async Task CreateOrder(Order newOrder)
        {
            newOrder.orderID = Guid.NewGuid().ToString("N");
            await _order.InsertOneAsync(newOrder);
        }
        public async Task UpdateOrder(string? id, Order updatedOrder)
        {
            updatedOrder.orderID = id;
            await _order.ReplaceOneAsync(m => m.orderID == id, updatedOrder);
        }

        public async Task<Order> GetOrderById(string? id) =>
            await _order.Find(o => o.orderID == id).FirstOrDefaultAsync();

        public async Task DeleteOrderById(string? id)
        {
            await _order.DeleteOneAsync(o => o.orderID == id);
        }
    }
}
