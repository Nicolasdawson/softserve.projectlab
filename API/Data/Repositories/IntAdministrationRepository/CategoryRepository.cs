using API.Data.Entities;
using API.Data.Repositories.IntAdministrationRepository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Data.Repositories.IntAdministrationRepository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _ctx;

        public CategoryRepository(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<CategoryEntity> AddAsync(CategoryEntity entity)
        {
            await _ctx.CategoryEntities.AddAsync(entity);
            await _ctx.SaveChangesAsync();
            return entity;
        }

        public async Task<CategoryEntity> UpdateAsync(CategoryEntity entity)
        {
            _ctx.CategoryEntities.Update(entity);
            await _ctx.SaveChangesAsync();
            return entity;
        }

        public async Task<CategoryEntity> GetByIdAsync(int id)
        {
            return await _ctx.CategoryEntities
                .Include(c => c.CatalogCategoryEntities)
                .FirstOrDefaultAsync(c => c.CategoryId == id && !c.IsDeleted);
        }



        public async Task<List<CategoryEntity>> GetAllAsync()
        {
            return await _ctx.CategoryEntities
                .Where(c => !c.IsDeleted)
                .ToListAsync();
        }

        public Task<bool> ExistsAsync(int id)
        {
            return _ctx.CategoryEntities.AnyAsync(c => c.CategoryId == id && !c.IsDeleted);
        }
    }
}
