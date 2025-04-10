using API.Data.Entities;
using API.Models;
using API.Models.IntAdmin;
using System.Collections.Generic;
using System.Threading.Tasks;
using softserve.projectlabs.Shared.Utilities;

namespace API.Services.IntAdmin
{
    /// <summary>
    /// Service interface for category operations.
    /// </summary>
    public interface ICategoryService
    {
        Task<Result<Category>> AddCategoryAsync(Category category);
        Task<Result<Category>> UpdateCategoryAsync(Category category);
        Task<Result<Category>> GetCategoryByIdAsync(int categoryId);
        Task<Result<List<Category>>> GetAllCategoriesAsync();
        Task<Result<bool>> RemoveCategoryAsync(int categoryId);
    }
}
