using API.Implementations.Domain;
using API.Models;
using API.Models.IntAdmin;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Services.IntAdmin
{
    /// <summary>
    /// Service class for category operations. Delegates business logic to the CategoryDomain.
    /// </summary>
    public class CategoryService : ICategoryService
    {
        private readonly CategoryDomain _categoryDomain;

        /// <summary>
        /// Constructor with dependency injection for CategoryDomain.
        /// </summary>
        /// <param name="categoryDomain">Injected CategoryDomain instance</param>
        public CategoryService(CategoryDomain categoryDomain)
        {
            _categoryDomain = categoryDomain;
        }

        public async Task<Result<Category>> AddCategoryAsync(Category category)
        {
            return await _categoryDomain.CreateCategory(category);
        }

        public async Task<Result<Category>> UpdateCategoryAsync(Category category)
        {
            return await _categoryDomain.UpdateCategory(category);
        }

        public async Task<Result<Category>> GetCategoryByIdAsync(int categoryId)
        {
            return await _categoryDomain.GetCategoryById(categoryId);
        }

        public async Task<Result<List<Category>>> GetAllCategoriesAsync()
        {
            return await _categoryDomain.GetAllCategories();
        }

        public async Task<Result<bool>> RemoveCategoryAsync(int categoryId)
        {
            return await _categoryDomain.RemoveCategory(categoryId);
        }
    }
}
