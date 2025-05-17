using API.Data;
using API.Data.Entities;
using API.Data.Repositories.IntAdministrationRepository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Repositories.IntAdministrationRepository;

public class ItemRepository : IItemRepository
{
    private readonly ApplicationDbContext _ctx;

    public ItemRepository(ApplicationDbContext ctx)
    {
        _ctx = ctx;
    }

    public async Task<ItemEntity?> GetByIdAsync(int id)
    {
        return await _ctx.ItemEntities.FirstOrDefaultAsync(i => i.ItemId == id);
    }

    public async Task<ItemEntity?> GetBySkuAsync(int sku)
    {
        return await _ctx.ItemEntities
            .FirstOrDefaultAsync(i => i.Sku == sku && !i.IsDeleted);
    }

    public async Task<List<ItemEntity>> GetAllAsync()
    {
        return await _ctx.ItemEntities.ToListAsync();
    }

    public async Task<ItemEntity> AddAsync(ItemEntity entity)
    {
        await _ctx.ItemEntities.AddAsync(entity);
        await _ctx.SaveChangesAsync();
        return entity;
    }

    public async Task<ItemEntity> UpdateAsync(ItemEntity entity)
    {
        _ctx.ItemEntities.Update(entity);
        await _ctx.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        if (entity == null) return false;

        _ctx.ItemEntities.Remove(entity);
        await _ctx.SaveChangesAsync();
        return true;
    }

    public Task<bool> ExistsAsync(int id)
    {
        return _ctx.ItemEntities.AnyAsync(i => i.ItemId == id && !i.IsDeleted);
    }
}
