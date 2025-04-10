using API.Data.Entities;
using API.Implementations.Domain;
using API.Models;
using API.Models.IntAdmin;
using API.Models.IntAdmin.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using softserve.projectlabs.Shared.Utilities;

namespace API.Services.IntAdmin
{
    /// <summary>
    /// Service class for role operations. Delegates business logic to the RoleDomain.
    /// </summary>
    public class RoleService : IRoleService
    {
        private readonly RoleDomain _roleDomain;

        /// <summary>
        /// Constructor with dependency injection for RoleDomain.
        /// </summary>
        public RoleService(RoleDomain roleDomain)
        {
            _roleDomain = roleDomain;
        }

        public async Task<Result<Role>> AddRoleAsync(Role role)
        {
            return await _roleDomain.CreateRole(role);
        }

        public async Task<Result<Role>> UpdateRoleAsync(Role role)
        {
            return await _roleDomain.UpdateRole(role);
        }

        public async Task<Result<Role>> GetRoleByIdAsync(int roleId)
        {
            return await _roleDomain.GetRoleById(roleId);
        }

        public async Task<Result<List<Role>>> GetAllRolesAsync()
        {
            return await _roleDomain.GetAllRoles();
        }

        public async Task<Result<bool>> RemoveRoleAsync(int roleId)
        {
            return await _roleDomain.RemoveRole(roleId);
        }

        public async Task<Result<bool>> AddPermissionToRoleAsync(int roleId, IPermission permission)
        {
            return await _roleDomain.AddPermissionToRole(roleId, permission);
        }

        public async Task<Result<bool>> RemovePermissionFromRoleAsync(int roleId, int permissionId)
        {
            return await _roleDomain.RemovePermissionFromRole(roleId, permissionId);
        }
    }
}
