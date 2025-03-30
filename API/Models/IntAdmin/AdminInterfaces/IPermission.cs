using API.Data.Entities;
using System.Collections.Generic;

namespace API.Models.IntAdmin.Interfaces
{
    public interface IPermission
    {
        int PermissionId { get; set; }
        string PermissionName { get; set; }
        string PermissionDescription { get; set; }

        /// <summary>
        /// Adds a new permission.
        /// </summary>
        /// <param name="permission">Permission object to add</param>
        /// <returns>Result containing the added permission</returns>
        Result<IPermission> AddPermission(IPermission permission);

        /// <summary>
        /// Updates an existing permission.
        /// </summary>
        /// <param name="permission">Permission object with updated data</param>
        /// <returns>Result containing the updated permission</returns>
        Result<IPermission> UpdatePermission(IPermission permission);

        /// <summary>
        /// Retrieves a permission by its ID.
        /// </summary>
        /// <param name="permissionId">Permission ID</param>
        /// <returns>Result containing the requested permission</returns>
        Result<IPermission> GetPermissionById(int permissionId);

        /// <summary>
        /// Retrieves all permissions.
        /// </summary>
        /// <returns>Result containing a list of permissions</returns>
        Result<List<IPermission>> GetAllPermissions();

        /// <summary>
        /// Removes a permission by its ID.
        /// </summary>
        /// <param name="permissionId">Permission ID</param>
        /// <returns>Result indicating success or failure</returns>
        Result<bool> RemovePermission(int permissionId);
    }
}
