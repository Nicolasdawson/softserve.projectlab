using API.Implementations.Domain;
using API.Models;
using API.Models.IntAdmin;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Services.IntAdmin
{
    /// <summary>
    /// Service class for permission operations. Delegates business logic to the PermissionDomain.
    /// </summary>
    public class PermissionService : IPermissionService
    {
        private readonly PermissionDomain _permissionDomain;

        /// <summary>
        /// Constructor with dependency injection for PermissionDomain.
        /// </summary>
        public PermissionService(PermissionDomain permissionDomain)
        {
            _permissionDomain = permissionDomain;
        }

        public async Task<Result<Permission>> AddPermissionAsync(Permission permission)
        {
            return await _permissionDomain.CreatePermission(permission);
        }

        public async Task<Result<Permission>> UpdatePermissionAsync(Permission permission)
        {
            return await _permissionDomain.UpdatePermission(permission);
        }

        public async Task<Result<Permission>> GetPermissionByIdAsync(int permissionId)
        {
            return await _permissionDomain.GetPermissionById(permissionId);
        }

        public async Task<Result<List<Permission>>> GetAllPermissionsAsync()
        {
            return await _permissionDomain.GetAllPermissions();
        }

        public async Task<Result<bool>> RemovePermissionAsync(int permissionId)
        {
            return await _permissionDomain.RemovePermission(permissionId);
        }
    }
}
