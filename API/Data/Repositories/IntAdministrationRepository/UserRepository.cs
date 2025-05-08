using API.Data;
using API.Data.Entities;
using API.Data.Repositories.IntAdministrationRepository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data.Repositories.IntAdministrationRepository;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _ctx;

    public UserRepository(ApplicationDbContext ctx)
    {
        _ctx = ctx;
    }

    public async Task<UserEntity> AddAsync(UserEntity entity)
    {
        await _ctx.UserEntities.AddAsync(entity);
        await _ctx.SaveChangesAsync();

        return await _ctx.UserEntities
            .Include(u => u.UserRoleEntities)
                .ThenInclude(ur => ur.Role)
            .FirstAsync(u => u.UserId == entity.UserId);
    }

    public async Task<UserEntity> UpdateAsync(UserEntity entity)
    {
        _ctx.UserEntities.Update(entity);
        await _ctx.SaveChangesAsync();
        return entity;
    }

    public async Task<UserEntity?> GetByIdAsync(int userId)
    {
        return await _ctx.UserEntities
            .Include(u => u.UserRoleEntities)
                .ThenInclude(ur => ur.Role)
            .FirstOrDefaultAsync(u => u.UserId == userId && !u.IsDeleted);
    }

    public async Task<List<UserEntity>> GetAllAsync()
    {
        return await _ctx.UserEntities
            .Where(u => !u.IsDeleted)
            .Include(u => u.UserRoleEntities)
                .ThenInclude(ur => ur.Role)
            .ToListAsync();
    }

    public async Task DeleteAsync(int userId)
    {
        var user = await _ctx.UserEntities.FirstOrDefaultAsync(u => u.UserId == userId);
        if (user != null)
        {
            user.IsDeleted = true;
            user.UpdatedAt = DateTime.UtcNow;
            await _ctx.SaveChangesAsync();
        }
    }

    public Task<bool> ExistsAsync(int userId)
    {
        return _ctx.UserEntities.AnyAsync(u => u.UserId == userId && !u.IsDeleted);
    }

    // Tabla pivote: UserRole
    public Task<List<UserRoleEntity>> GetUserRolesAsync(int userId)
    {
        return _ctx.UserRoleEntities
            .Where(ur => ur.UserId == userId)
            .ToListAsync();
    }

    public async Task AddUserRoleAsync(UserRoleEntity pivot)
    {
        await _ctx.UserRoleEntities.AddAsync(pivot);
        await _ctx.SaveChangesAsync();
    }

    public async Task RemoveUserRoleAsync(UserRoleEntity pivot)
    {
        _ctx.UserRoleEntities.Remove(pivot);
        await _ctx.SaveChangesAsync();
    }

    public async Task<UserEntity?> GetByEmailAsync(string email)
    {
        return await _ctx.UserEntities
            .Include(u => u.UserRoleEntities)
                .ThenInclude(ur => ur.Role)
            .FirstOrDefaultAsync(u => u.UserContactEmail == email && !u.IsDeleted);
    }
}
