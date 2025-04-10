using API.Data.Entities;
using API.Models;
using API.Models.IntAdmin;
using API.Models.IntAdmin.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using softserve.projectlabs.Shared.Utilities;

namespace API.Implementations.Domain
{
    /// <summary>
    /// Domain class for handling Role operations.
    /// Uses in-memory storage for roles.
    /// </summary>
    public class RoleDomain
    {
        // In-memory storage for roles
        private readonly List<Role> _roles = new List<Role>();

        /// <summary>
        /// Creates a new role and adds it to the in-memory list.
        /// </summary>
        /// <param name="role">Role object to be created</param>
        /// <returns>Result object containing the created role</returns>
        public async Task<Result<Role>> CreateRole(Role role)
        {
            try
            {
                _roles.Add(role);
                return Result<Role>.Success(role);
            }
            catch (Exception ex)
            {
                return Result<Role>.Failure($"Failed to create role: {ex.Message}");
            }
        }

        /// <summary>
        /// Updates an existing role if found.
        /// </summary>
        /// <param name="role">Role object with updated information</param>
        /// <returns>Result object containing the updated role</returns>
        public async Task<Result<Role>> UpdateRole(Role role)
        {
            try
            {
                var existingRole = _roles.FirstOrDefault(r => r.RoleId == role.RoleId);
                if (existingRole != null)
                {
                    existingRole.RoleName = role.RoleName;
                    existingRole.RoleDescription = role.RoleDescription;
                    existingRole.RoleStatus = role.RoleStatus;
                    existingRole.Permissions = role.Permissions; // update if needed
                    return Result<Role>.Success(existingRole);
                }
                else
                {
                    return Result<Role>.Failure("Role not found.");
                }
            }
            catch (Exception ex)
            {
                return Result<Role>.Failure($"Failed to update role: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves a role by its unique ID.
        /// </summary>
        /// <param name="roleId">Unique identifier of the role</param>
        /// <returns>Result object containing the role if found, otherwise an error</returns>
        public async Task<Result<Role>> GetRoleById(int roleId)
        {
            try
            {
                var role = _roles.FirstOrDefault(r => r.RoleId == roleId);
                return role != null
                    ? Result<Role>.Success(role)
                    : Result<Role>.Failure("Role not found.");
            }
            catch (Exception ex)
            {
                return Result<Role>.Failure($"Failed to get role: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves all roles stored in memory.
        /// </summary>
        /// <returns>Result object containing a list of roles</returns>
        public async Task<Result<List<Role>>> GetAllRoles()
        {
            try
            {
                return Result<List<Role>>.Success(_roles);
            }
            catch (Exception ex)
            {
                return Result<List<Role>>.Failure($"Failed to retrieve roles: {ex.Message}");
            }
        }

        /// <summary>
        /// Removes a role by its unique ID.
        /// </summary>
        /// <param name="roleId">Unique identifier of the role to remove</param>
        /// <returns>Result object indicating success or failure</returns>
        public async Task<Result<bool>> RemoveRole(int roleId)
        {
            try
            {
                var roleToRemove = _roles.FirstOrDefault(r => r.RoleId == roleId);
                if (roleToRemove != null)
                {
                    _roles.Remove(roleToRemove);
                    return Result<bool>.Success(true);
                }
                else
                {
                    return Result<bool>.Failure("Role not found.");
                }
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure($"Failed to remove role: {ex.Message}");
            }
        }

        /// <summary>
        /// Adds a permission to the role's Permissions list.
        /// </summary>
        /// <param name="roleId">ID of the role</param>
        /// <param name="permission">Permission object to add</param>
        /// <returns>Result object indicating success or failure</returns>
        public async Task<Result<bool>> AddPermissionToRole(int roleId, IPermission permission)
        {
            try
            {
                var existingRole = _roles.FirstOrDefault(r => r.RoleId == roleId);
                if (existingRole != null)
                {
                    existingRole.Permissions.Add(permission);
                    return Result<bool>.Success(true);
                }
                return Result<bool>.Failure("Role not found.");
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure($"Failed to add permission: {ex.Message}");
            }
        }

        /// <summary>
        /// Removes a permission from the role's Permissions list by permission ID.
        /// </summary>
        /// <param name="roleId">ID of the role</param>
        /// <param name="permissionId">ID of the permission to remove</param>
        /// <returns>Result object indicating success or failure</returns>
        public async Task<Result<bool>> RemovePermissionFromRole(int roleId, int permissionId)
        {
            try
            {
                var existingRole = _roles.FirstOrDefault(r => r.RoleId == roleId);
                if (existingRole != null)
                {
                    var permissionToRemove = existingRole.Permissions.FirstOrDefault(p => p.PermissionId == permissionId);
                    if (permissionToRemove != null)
                    {
                        existingRole.Permissions.Remove(permissionToRemove);
                        return Result<bool>.Success(true);
                    }
                    return Result<bool>.Failure("Permission not found in role.");
                }
                return Result<bool>.Failure("Role not found.");
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure($"Failed to remove permission: {ex.Message}");
            }
        }
    }
}
