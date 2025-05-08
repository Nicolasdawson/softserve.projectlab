using API.Data;
using API.Data.Entities;
using API.Data.Repositories.LogisticsRepositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Repositories.LogisticsRepositories
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly ApplicationDbContext _context;

        public SupplierRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<SupplierEntity?> GetByIdAsync(int supplierId)
        {
            return await _context.SupplierEntities
                .Include(s => s.SupplierItemEntities) 
                .FirstOrDefaultAsync(s => s.SupplierId == supplierId);
        }

        public async Task<List<SupplierEntity>> GetAllAsync()
        {
            return await _context.SupplierEntities
                .Include(s => s.SupplierItemEntities) 
                .ToListAsync();
        }

        public async Task AddAsync(SupplierEntity supplierEntity)
        {
            await _context.SupplierEntities.AddAsync(supplierEntity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(SupplierEntity supplierEntity)
        {
            _context.SupplierEntities.Update(supplierEntity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int supplierId)
        {
            var supplierEntity = await GetByIdAsync(supplierId);
            if (supplierEntity != null)
            {
                _context.SupplierEntities.Remove(supplierEntity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
