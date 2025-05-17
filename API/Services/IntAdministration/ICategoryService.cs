using API.Models.IntAdmin;
using softserve.projectlabs.Shared.DTOs.Category;
using softserve.projectlabs.Shared.Utilities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Services.IntAdmin
{
    public interface ICategoryService
    {
    Task<Result<Category>> CreateCategoryAsync(CategoryCreateDto categoryDto);
    Task<Result<Category>> UpdateCategoryAsync(int categoryId, CategoryUpdateDto categoryDto);
    Task<Result<Category>> GetCategoryByIdAsync(int categoryId);
    Task<Result<List<Category>>> GetAllCategoriesAsync();
    Task<Result<bool>> DeleteCategoryAsync(int categoryId);
    }
}
