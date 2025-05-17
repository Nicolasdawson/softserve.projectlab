namespace API.Implementations.Domain;

using API.Data.Repositories.IntAdministrationRepository.Interfaces;
using API.Models.IntAdmin;
using softserve.projectlabs.Shared.Utilities;
using System.Collections.Generic;
using System.Threading.Tasks;

public class PermissionDomain
{
    private readonly IPermissionRepository _repo;

    public PermissionDomain(IPermissionRepository repo)
    {
        _repo = repo;
    }

    public async Task<Result<Permission>> CreatePermissionAsync(Permission permission)
    {
        if (string.IsNullOrWhiteSpace(permission.PermissionName))
            return Result<Permission>.Failure("El nombre del permiso no puede estar vacío.");

        var created = await _repo.CreateAsync(permission);
        return Result<Permission>.Success(created);
    }

    public async Task<Result<Permission>> UpdatePermissionAsync(Permission permission)
    {
        var exists = await _repo.GetByIdAsync(permission.PermissionId);
        if (exists == null)
            return Result<Permission>.Failure("Permiso no encontrado.");

        var updated = await _repo.UpdateAsync(permission);
        return Result<Permission>.Success(updated);
    }

    public async Task<Result<Permission>> GetPermissionByIdAsync(int permissionId)
    {
        var model = await _repo.GetByIdAsync(permissionId);
        return model == null
            ? Result<Permission>.Failure("Permiso no encontrado.")
            : Result<Permission>.Success(model);
    }

    public async Task<Result<List<Permission>>> GetAllPermissionsAsync()
    {
        var list = await _repo.GetAllAsync();
        return Result<List<Permission>>.Success(list);
    }

    public async Task<Result<Permission>> DeletePermissionAsync(int permissionId)
    {
        var exists = await _repo.GetByIdAsync(permissionId);
        if (exists == null)
            return Result<Permission>.Failure("Permiso no encontrado.");

        var deleted = await _repo.DeleteAsync(permissionId);
        return Result<Permission>.Success(deleted);
    }
}
