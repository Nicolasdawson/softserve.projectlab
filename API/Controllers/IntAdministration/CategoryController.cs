using API.Services.IntAdmin;
using Microsoft.AspNetCore.Mvc;
using softserve.projectlabs.Shared.DTOs.Category;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers.IntAdmin
{
    [ApiController]
    [Route("api/categories")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryCreateDto dto)
        {
            var result = await _categoryService.CreateCategoryAsync(dto);
            return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
        }

        [HttpPut("{categoryId}")]
        public async Task<IActionResult> UpdateCategory(int categoryId, [FromBody] CategoryUpdateDto dto)
        {
            var result = await _categoryService.UpdateCategoryAsync(categoryId, dto);
            return result.IsSuccess ? Ok(result.Data) : NotFound(result.ErrorMessage);
        }

        [HttpGet("{categoryId}")]
        public async Task<IActionResult> GetCategoryById(int categoryId)
        {
            var result = await _categoryService.GetCategoryByIdAsync(categoryId);
            return result.IsSuccess ? Ok(result.Data) : NotFound(result.ErrorMessage);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var result = await _categoryService.GetAllCategoriesAsync();
            return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
        }

        [HttpDelete("{categoryId}")]
        public async Task<IActionResult> DeleteCategory(int categoryId)
        {
            var result = await _categoryService.DeleteCategoryAsync(categoryId);
            return result.IsSuccess ? NoContent() : NotFound(result.ErrorMessage);
        }
    }
}
