using DDServices.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DDServices.Services
{
    public class OutletService
    {
        private readonly IMongoCollection<Outlet> _outlet;

        public OutletService(IOptions<MongoDBSettings> options)
        {
            var mongoDBClient = new MongoClient(options.Value.ConnectionURI);

            _outlet = mongoDBClient.GetDatabase(options.Value.DatabaseName).GetCollection<Outlet>(options.Value.OutletCollectionName);
        }

        public async Task<List<Outlet>> GetOutletList() =>
            await _outlet.Find(_ => true).ToListAsync();

        public async Task<Outlet> GetOutletById(string outletId) =>
            await _outlet.Find(o => o.OutletID == outletId).FirstOrDefaultAsync();

    }
}
