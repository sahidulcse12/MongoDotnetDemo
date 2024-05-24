using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDotnetDemo.Models;
using MongoDotnetDemo.Services;

namespace MongoDotnetDemo.Controllers
{
    [Route("api/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> Get()
        {
            var categories = await _categoryService.GetCategoriesAsync();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category is null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Post(Category category)
        {
            await _categoryService.CreateCategory(category);
            return Ok("Category Created Successfully");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] Category newCategory)
        {
            var result = await _categoryService.GetCategoryByIdAsync(id);
            if (result is null)
            {
                return NotFound();
            }
            await _categoryService.UpdateCategory(id, newCategory);
            return Ok("Category Updated Successfully");
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _categoryService.GetCategoryByIdAsync(id);
            if (result is null) 
            { 
                return NotFound();
            }
            await _categoryService.DeleteCategory(id);
            return Ok("Category Deleted Successfully");
        }

    }
}
