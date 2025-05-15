using API.Data.Entities;

namespace API.Data.Repositories.IntAdministrationRepository.Interfaces;

public interface IItemRepository
{
    Task<ItemEntity> AddAsync(ItemEntity entity);
    Task<ItemEntity?> GetByIdAsync(int id);
    Task<List<ItemEntity>> GetAllAsync();
    Task<ItemEntity> UpdateAsync(ItemEntity entity);
    Task<bool> DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
}
