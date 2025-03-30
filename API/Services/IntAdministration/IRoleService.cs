using API.Data.Entities;
using API.Models;
using API.Models.IntAdmin;
using API.Models.IntAdmin.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Services.IntAdmin
{
    /// <summary>
    /// Service interface for role operations.
    /// </summary>
    public interface IRoleService
    {
        Task<Result<Role>> AddRoleAsync(Role role);
        Task<Result<Role>> UpdateRoleAsync(Role role);
        Task<Result<Role>> GetRoleByIdAsync(int roleId);
        Task<Result<List<Role>>> GetAllRolesAsync();
        Task<Result<bool>> RemoveRoleAsync(int roleId);

        // Permission management
        Task<Result<bool>> AddPermissionToRoleAsync(int roleId, IPermission permission);
        Task<Result<bool>> RemovePermissionFromRoleAsync(int roleId, int permissionId);
    }
}
