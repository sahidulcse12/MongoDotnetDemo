using MongoDotnetDemo.Models;

namespace MongoDotnetDemo.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetCategoriesAsync();
        Task<Category> GetCategoryByIdAsync(string id);
        Task CreateCategory(Category category);
        Task UpdateCategory(string id,Category category);
        Task DeleteCategory(string id);
    }
}
