using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDotnetDemo.Models;

namespace MongoDotnetDemo.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IMongoCollection<Category> _categoryCollections;
        private readonly IOptions<DatabaseSettings> _dbSettings;

        public CategoryService(IOptions<DatabaseSettings> dbSettings)
        {
            _dbSettings = dbSettings;
            var mongoClient = new MongoClient(dbSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(dbSettings.Value.DatabaseName);
            _categoryCollections = mongoDatabase.GetCollection<Category>
                (dbSettings.Value.CategoriesCollectionName);
        }
        public async Task CreateCategory(Category category)=>
            await _categoryCollections.InsertOneAsync(category);

        public async Task DeleteCategory(string id)=>
            await _categoryCollections.DeleteOneAsync(a=>a.Id==id);

        public async Task<IEnumerable<Category>> GetCategoriesAsync() =>
            await _categoryCollections.Find(_ => true).ToListAsync();

        public async Task<Category> GetCategoryByIdAsync(string id)=>
            await _categoryCollections.Find(a=>a.Id == id).FirstOrDefaultAsync();

        public async Task UpdateCategory(string id, Category category) =>
            await _categoryCollections.ReplaceOneAsync(a => a.Id == id, category);
    }
}
