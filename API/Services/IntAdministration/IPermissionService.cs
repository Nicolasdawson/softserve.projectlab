namespace API.Services.IntAdmin;

using softserve.projectlabs.Shared.DTOs;
using softserve.projectlabs.Shared.Utilities;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IPermissionService
{
    Task<Result<PermissionDto>> CreatePermissionAsync(PermissionDto dto);
    Task<Result<PermissionDto>> UpdatePermissionAsync(PermissionDto dto);
    Task<Result<PermissionDto>> GetPermissionByIdAsync(int permissionId);
    Task<Result<List<PermissionDto>>> GetAllPermissionsAsync();
    Task<Result<PermissionDto>> DeletePermissionAsync(int permissionId);
}
