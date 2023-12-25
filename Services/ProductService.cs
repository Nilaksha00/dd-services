using DDServices.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DDServices.Services
{
    public class ProductService
    {
        private readonly IMongoCollection<Product> _product;

        public ProductService(IOptions<MongoDBSettings> options)
        {
            var mongoDBClient = new MongoClient(options.Value.ConnectionURI);

            _product = mongoDBClient.GetDatabase(options.Value.DatabaseName).GetCollection<Product>(options.Value.ProductCollectionName);

        }

        public async Task<List<Product>> GetProductList() =>
         await _product.Find(_ => true).ToListAsync();

        public async Task CreateProduct(Product newProduct)  {
            newProduct.productID = Guid.NewGuid().ToString("N");
            await _product.InsertOneAsync(newProduct);
    }

    }
}
