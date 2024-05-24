using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDotnetDemo.Models;

namespace MongoDotnetDemo.Services
{
    public class ProductService : IProductService
    {

        private readonly IMongoCollection<Product> _productCollections;
        private readonly IOptions<DatabaseSettings> _dbSettings;

        public ProductService(IOptions<DatabaseSettings> dbSettings)
        {
            _dbSettings = dbSettings;
            var mongoClient = new MongoClient(dbSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(dbSettings.Value.DatabaseName);
            _productCollections = mongoDatabase.GetCollection<Product>
                (dbSettings.Value.ProductsCollectionName);
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            // Define the pipeline for the aggregation query
            var pipeline = new BsonDocument[]
            {
                new BsonDocument("$lookup", new BsonDocument
                {
                    { "from", "CategoryCollection" },
                    { "localField", "CategoryId" },
                    { "foreignField", "_id" },
                    { "as", "product_category" }
                }),
                new BsonDocument("$unwind", "$product_category"),
                new BsonDocument("$project", new BsonDocument
                {
                    { "_id", 1 },
                    { "CategoryId", 1},
                    { "ProductName",1 },
                    { "CategoryName", "$product_category.CategoryName" }
                })
            };

            var result = await _productCollections.Aggregate<Product>(pipeline).ToListAsync();
            return result;
        }

        public async Task AddProductAsync(Product product)=>
            await _productCollections.InsertOneAsync(product);

        public async Task DeleteProductAsync(string id)=>
            await _productCollections.DeleteOneAsync(a=>a.Id==id);

        public async Task<Product> GetProductByIdAsync(string id)=>
            await _productCollections.Find(a=>a.Id == id).FirstOrDefaultAsync();

        public async Task UpdateProductAsync(string id,Product product)=>
            await _productCollections.ReplaceOneAsync(a=>a.Id==id, product);
    }
}
