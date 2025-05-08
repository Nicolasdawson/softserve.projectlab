using API.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Data.Repositories.IntAdministrationRepository.Interfaces
{
    public interface ICategoryRepository
    {
        Task<CategoryEntity> GetByIdAsync(int id);
        Task<List<CategoryEntity>> GetAllAsync();
        Task<CategoryEntity> AddAsync(CategoryEntity entity);
        Task<CategoryEntity> UpdateAsync(CategoryEntity entity);
        Task<bool> ExistsAsync(int id);
    }
}
