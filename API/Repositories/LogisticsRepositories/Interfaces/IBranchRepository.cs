using API.Data.Entities;

namespace API.Repositories.LogisticsRepositories.Interfaces
{
    public interface IBranchRepository
    {
        Task<BranchEntity?> GetByIdAsync(int branchId);
        Task<List<BranchEntity>> GetAllAsync();
        Task AddAsync(BranchEntity branchEntity);
        Task UpdateAsync(BranchEntity branchEntity);
        Task DeleteAsync(int branchId);
        Task<BranchEntity?> GetByNameAndCityAsync(string name, string city);
    }
}
