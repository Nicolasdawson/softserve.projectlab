using API.Data.Models;
using API.Data.Models.DTOs.Category;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Repository.IRepository
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<CategoryGetDto>> GetCategories();
        Task<bool> AddCategory(CategoryCreateDto categoryCreateDto);
        Task<bool> DeleteCategory(int id);
    }
}
