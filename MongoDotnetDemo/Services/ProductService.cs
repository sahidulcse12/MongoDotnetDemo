using MongoDotnetDemo.Models;

namespace MongoDotnetDemo.Services
{
    public class ProductService : IProductService
    {
        public Task AddProductAsync(Product product)
        {
            throw new NotImplementedException();
        }

        public Task DeleteProductAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Product> GetProductByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetProductsAsync()
        {
            throw new NotImplementedException();
        }

        public Task UpdateProductAsync(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
