using API.Data;
using API.Data.Entities;
using API.Data.Repositories.LogisticsRepositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Repositories.LogisticsRepositories
{
    public class BranchRepository : IBranchRepository
    {
        private readonly ApplicationDbContext _context;

        public BranchRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<BranchEntity?> GetByIdAsync(int branchId)
        {
            return await _context.BranchEntities
                .FirstOrDefaultAsync(b => b.BranchId == branchId && !b.IsDeleted);
        }

        public async Task<List<BranchEntity>> GetAllAsync()
        {
            return await _context.BranchEntities
                .Where(b => !b.IsDeleted)
                .ToListAsync();
        }

        public async Task AddAsync(BranchEntity branchEntity)
        {
            await _context.BranchEntities.AddAsync(branchEntity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(BranchEntity branchEntity)
        {
            _context.BranchEntities.Update(branchEntity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int branchId)
        {
            var branchEntity = await GetByIdAsync(branchId);
            if (branchEntity != null)
            {
                branchEntity.IsDeleted = true;
                branchEntity.UpdatedAt = DateTime.UtcNow;
                _context.BranchEntities.Update(branchEntity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<BranchEntity?> GetByNameAndCityAsync(string name, string city)
        {
            return await _context.BranchEntities
                .Where(b => !b.IsDeleted &&
                            b.BranchName.ToLower() == name.ToLower() &&
                            b.BranchCity.ToLower() == city.ToLower())
                .FirstOrDefaultAsync();
        }
    }
}
