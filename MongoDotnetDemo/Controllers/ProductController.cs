using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDotnetDemo.Models;
using MongoDotnetDemo.Services;

namespace MongoDotnetDemo.Controllers
{
    [Route("api/")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> Get()
        {
            var products = await _productService.GetProductsAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product is null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Post(Product product)
        {
            await _productService.AddProductAsync(product);
            return Ok("Product Created Successfully");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] Product newProduct)
        {
            newProduct.CategoryName = null;
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
                return NotFound();
            await _productService.UpdateProductAsync(id, newProduct);
            return Ok("updated successfully");
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _productService.GetProductByIdAsync(id);
            if (result is null)
            {
                return NotFound();
            }
            await _productService.DeleteProductAsync(id);
            return Ok("Product Deleted Successfully");
        }

    }
}

