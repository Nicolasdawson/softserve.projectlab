using API.Data.Entities;

namespace API.Repositories.LogisticsRepositories.Interfaces
{
    public interface IWarehouseRepository
    {
        Task<WarehouseEntity?> GetByIdAsync(int warehouseId);
        Task<List<WarehouseEntity>> GetAllAsync();
        Task AddAsync(WarehouseEntity warehouseEntity);
        Task UpdateAsync(WarehouseEntity warehouseEntity);
        Task DeleteAsync(int warehouseId);
        Task<WarehouseEntity?> GetByNameAsync(string name);
    }
}
