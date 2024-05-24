using MongoDotnetDemo.Models;

namespace MongoDotnetDemo.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetProductsAsync();
        Task<Product> GetProductByIdAsync(string id);
        Task AddProductAsync(Product product);
        Task UpdateProductAsync(string id,Product product);
        Task DeleteProductAsync(string id);
    }
}
