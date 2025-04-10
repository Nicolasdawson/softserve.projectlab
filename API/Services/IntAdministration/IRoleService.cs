using API.Models;
using API.Models.DTOs;
using API.Models.IntAdmin;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Services.IntAdmin
{
    /// <summary>
    /// Service interface for role operations.
    /// </summary>
    public interface IRoleService
    {
        Task<Result<Role>> CreateRoleAsync(RoleDto roleDto);
        Task<Result<Role>> UpdateRoleAsync(int roleId, RoleDto roleDto);
        Task<Result<Role>> GetRoleByIdAsync(int roleId);
        Task<Result<List<Role>>> GetAllRolesAsync();
        Task<Result<bool>> DeleteRoleAsync(int roleId);

        // Permission management
        Task<Result<bool>> AddPermissionsToRoleAsync(int roleId, List<int> permissionIds);
        Task<Result<bool>> RemovePermissionFromRoleAsync(int roleId, int permissionId);
    }
}
