using API.Models.IntAdmin;
using API.Services.IntAdmin;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace API.Controllers.IntAdmin
{
    /// <summary>
    /// API Controller for managing Category operations.
    /// </summary>
    [ApiController]
    [Route("api/categories")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        /// <summary>
        /// Constructor with dependency injection for ICategoryService.
        /// </summary>
        /// <param name="categoryService">Injected ICategoryService instance</param>
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        /// <summary>
        /// Adds a new category.
        /// </summary>
        /// <param name="category">Category object to add</param>
        /// <returns>HTTP response with the created category or error message</returns>
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] Category category)
        {
            var result = await _categoryService.AddCategoryAsync(category);
            return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
        }

        /// <summary>
        /// Updates an existing category.
        /// </summary>
        /// <param name="category">Category object with updated data</param>
        /// <returns>HTTP response with the updated category or error message</returns>
        [HttpPut("{categoryId}")]
        public async Task<IActionResult> UpdateCategory(int categoryId, [FromBody] Category category)
        {
            category.CategoryId = categoryId;
            var result = await _categoryService.UpdateCategoryAsync(category);
            return result.IsSuccess ? Ok(result.Data) : NotFound(result.ErrorMessage);
        }

        /// <summary>
        /// Retrieves a category by its unique ID.
        /// </summary>
        /// <param name="categoryId">Unique identifier of the category</param>
        /// <returns>HTTP response with the category or error message</returns>
        [HttpGet("{categoryId}")]
        public async Task<IActionResult> GetCategoryById(int categoryId)
        {
            var result = await _categoryService.GetCategoryByIdAsync(categoryId);
            return result.IsSuccess ? Ok(result.Data) : NotFound(result.ErrorMessage);
        }

        /// <summary>
        /// Retrieves all categories.
        /// </summary>
        /// <returns>HTTP response with the list of categories or error message</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var result = await _categoryService.GetAllCategoriesAsync();
            return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
        }

        /// <summary>
        /// Removes a category by its unique ID.
        /// </summary>
        /// <param name="categoryId">Unique identifier of the category to remove</param>
        /// <returns>HTTP response indicating success or failure</returns>
        [HttpDelete("{categoryId}")]
        public async Task<IActionResult> RemoveCategory(int categoryId)
        {
            var result = await _categoryService.RemoveCategoryAsync(categoryId);
            if (result.IsNoContent)
            {
                return NoContent();
            }
            return result.IsSuccess ? Ok(result.Data) : NotFound(result.ErrorMessage);
        }
    }
}
