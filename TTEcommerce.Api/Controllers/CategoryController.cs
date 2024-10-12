using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TTEcommerce.Application.Dtos;
using TTEcommerce.Application.Interfaces;

namespace TTEcommerce.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDto>> GetCategoryById(string id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> SearchCategories([FromQuery] string searchTerm)
        {
            var categories = await _categoryService.SearchCategoriesAsync(searchTerm);
            return Ok(categories);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetPaginatedCategories([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            var categories = await _categoryService.GetPaginatedCategoriesAsync(pageNumber, pageSize);
            return Ok(categories);
        }

        [HttpPost]
        public async Task<ActionResult> CreateCategory([FromBody] CategoryDto categoryDto)
        {
            await _categoryService.CreateCategoryAsync(categoryDto);
            return CreatedAtAction(nameof(GetCategoryById), new { id = categoryDto.Id }, categoryDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCategory(string id, [FromBody] CategoryDto categoryDto)
        {
            await _categoryService.UpdateCategoryAsync(id, categoryDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCategory(string id)
        {
            await _categoryService.DeleteCategoryAsync(id);
            return NoContent();
        }
    }
}