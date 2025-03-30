using API.Data.Entities;
using API.Models;
using API.Models.IntAdmin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Implementations.Domain
{
    /// <summary>
    /// Domain class for handling Permission operations.
    /// Uses in-memory storage for permissions.
    /// </summary>
    public class PermissionDomain
    {
        // In-memory storage for permissions
        private readonly List<Permission> _permissions = new List<Permission>();

        /// <summary>
        /// Creates a new permission and adds it to the in-memory list.
        /// </summary>
        /// <param name="permission">Permission object to be created</param>
        /// <returns>Result object containing the created permission</returns>
        public async Task<Result<Permission>> CreatePermission(Permission permission)
        {
            try
            {
                _permissions.Add(permission);
                return Result<Permission>.Success(permission);
            }
            catch (Exception ex)
            {
                return Result<Permission>.Failure($"Failed to create permission: {ex.Message}");
            }
        }

        /// <summary>
        /// Updates an existing permission if found.
        /// </summary>
        /// <param name="permission">Permission object with updated information</param>
        /// <returns>Result object containing the updated permission</returns>
        public async Task<Result<Permission>> UpdatePermission(Permission permission)
        {
            try
            {
                var existingPermission = _permissions.FirstOrDefault(p => p.PermissionId == permission.PermissionId);
                if (existingPermission != null)
                {
                    existingPermission.PermissionName = permission.PermissionName;
                    existingPermission.PermissionDescription = permission.PermissionDescription;
                    return Result<Permission>.Success(existingPermission);
                }
                else
                {
                    return Result<Permission>.Failure("Permission not found.");
                }
            }
            catch (Exception ex)
            {
                return Result<Permission>.Failure($"Failed to update permission: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves a permission by its unique ID.
        /// </summary>
        /// <param name="permissionId">Unique identifier of the permission</param>
        /// <returns>Result object containing the permission if found, otherwise an error</returns>
        public async Task<Result<Permission>> GetPermissionById(int permissionId)
        {
            try
            {
                var permission = _permissions.FirstOrDefault(p => p.PermissionId == permissionId);
                return permission != null
                    ? Result<Permission>.Success(permission)
                    : Result<Permission>.Failure("Permission not found.");
            }
            catch (Exception ex)
            {
                return Result<Permission>.Failure($"Failed to get permission: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves all permissions stored in memory.
        /// </summary>
        /// <returns>Result object containing a list of permissions</returns>
        public async Task<Result<List<Permission>>> GetAllPermissions()
        {
            try
            {
                return Result<List<Permission>>.Success(_permissions);
            }
            catch (Exception ex)
            {
                return Result<List<Permission>>.Failure($"Failed to retrieve permissions: {ex.Message}");
            }
        }

        /// <summary>
        /// Removes a permission by its unique ID.
        /// </summary>
        /// <param name="permissionId">Unique identifier of the permission to remove</param>
        /// <returns>Result object indicating success or failure</returns>
        public async Task<Result<bool>> RemovePermission(int permissionId)
        {
            try
            {
                var permissionToRemove = _permissions.FirstOrDefault(p => p.PermissionId == permissionId);
                if (permissionToRemove != null)
                {
                    _permissions.Remove(permissionToRemove);
                    return Result<bool>.Success(true);
                }
                else
                {
                    return Result<bool>.Failure("Permission not found.");
                }
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure($"Failed to remove permission: {ex.Message}");
            }
        }
    }
}
