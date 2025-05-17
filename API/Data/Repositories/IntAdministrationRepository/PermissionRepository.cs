namespace API.Data.Repositories.IntAdministrationRepository;

using API.Data;
using API.Data.Entities;
using API.Data.Repositories.IntAdministrationRepository.Interfaces;
using API.Models.IntAdmin;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class PermissionRepository : IPermissionRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public PermissionRepository(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Permission> CreateAsync(Permission permission)
    {
        var entity = _mapper.Map<PermissionEntity>(permission);
        entity.CreatedAt = DateTime.UtcNow;
        entity.UpdatedAt = DateTime.UtcNow;
        entity.IsDeleted = false;

        _context.PermissionEntities.Add(entity);
        await _context.SaveChangesAsync();

        return _mapper.Map<Permission>(entity);
    }

    public async Task<Permission> UpdateAsync(Permission permission)
    {
        var entity = await _context.PermissionEntities
            .FirstOrDefaultAsync(p => p.PermissionId == permission.PermissionId && !p.IsDeleted);
        if (entity == null) return null;

        _mapper.Map(permission, entity);
        entity.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();

        return _mapper.Map<Permission>(entity);
    }

    public async Task<Permission> GetByIdAsync(int permissionId)
    {
        var entity = await _context.PermissionEntities
            .FirstOrDefaultAsync(p => p.PermissionId == permissionId && !p.IsDeleted);
        return entity == null
            ? null
            : _mapper.Map<Permission>(entity);
    }

    public async Task<List<Permission>> GetAllAsync()
    {
        var entities = await _context.PermissionEntities
            .Where(p => !p.IsDeleted)
            .ToListAsync();
        return _mapper.Map<List<Permission>>(entities);
    }

    public async Task<Permission> DeleteAsync(int permissionId)
    {
        var entity = await _context.PermissionEntities
            .FirstOrDefaultAsync(p => p.PermissionId == permissionId && !p.IsDeleted);
        if (entity == null) return null;

        entity.IsDeleted = true;
        entity.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();

        return _mapper.Map<Permission>(entity);
    }
}
