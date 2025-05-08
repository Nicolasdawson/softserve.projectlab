using API.Data.Entities;

namespace API.Data.Repositories.LogisticsRepositories.Interfaces
{
    public interface ISupplierRepository
    {
        Task<SupplierEntity?> GetByIdAsync(int supplierId);
        Task<List<SupplierEntity>> GetAllAsync();
        Task AddAsync(SupplierEntity supplierEntity);
        Task UpdateAsync(SupplierEntity supplierEntity);
        Task DeleteAsync(int supplierId);
    }
}
