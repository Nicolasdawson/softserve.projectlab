using API.Data.Entities;
using API.Data.Repositories.IntAdministrationRepository.Interfaces;
using API.Data;
using Microsoft.EntityFrameworkCore;

public class CatalogRepository : ICatalogRepository
{
    private readonly ApplicationDbContext _ctx;

    public CatalogRepository(ApplicationDbContext ctx)
    {
        _ctx = ctx;
    }

    public async Task<CatalogEntity> AddAsync(CatalogEntity entity)
    {
        await _ctx.CatalogEntities.AddAsync(entity);
        await _ctx.SaveChangesAsync();
        return await _ctx.CatalogEntities
            .Include(c => c.CatalogCategoryEntities)
            .FirstAsync(c => c.CatalogId == entity.CatalogId);
    }

    public async Task<CatalogEntity> UpdateAsync(CatalogEntity entity)
    {
        _ctx.CatalogEntities.Update(entity);
        await _ctx.SaveChangesAsync();
        return entity;
    }

    public async Task<CatalogEntity?> GetByIdAsync(int id)
    {
        return await _ctx.CatalogEntities
            .Include(c => c.CatalogCategoryEntities)
                .ThenInclude(cc => cc.Category)
            .FirstOrDefaultAsync(c => c.CatalogId == id && !c.IsDeleted);
    }


    public async Task<List<CatalogEntity>> GetAllAsync()
    {
        return await _ctx.CatalogEntities
            .Where(c => !c.IsDeleted)
            .Include(c => c.CatalogCategoryEntities)
                .ThenInclude(cc => cc.Category) // <--- ESTA LÍNEA ES CLAVE
            .ToListAsync();
    }


    public Task<bool> ExistsAsync(int id)
        => _ctx.CatalogEntities.AnyAsync(c => c.CatalogId == id && !c.IsDeleted);

    public async Task DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        if (entity != null)
        {
            entity.IsDeleted = true;
            entity.UpdatedAt = DateTime.UtcNow;
            await _ctx.SaveChangesAsync();
        }
    }

    // Métodos pivote
    public Task<List<CatalogCategoryEntity>> GetCatalogCategoriesAsync(int catalogId)
    {
        return _ctx.CatalogCategoryEntities
            .Where(cc => cc.CatalogId == catalogId)
            .ToListAsync();
    }

    public async Task AddCatalogCategoryAsync(CatalogCategoryEntity pivot)
    {
        await _ctx.CatalogCategoryEntities.AddAsync(pivot);
        await _ctx.SaveChangesAsync();
    }

    public async Task RemoveCatalogCategoryAsync(CatalogCategoryEntity pivot)
    {
        _ctx.CatalogCategoryEntities.Remove(pivot);
        await _ctx.SaveChangesAsync();
    }
}
