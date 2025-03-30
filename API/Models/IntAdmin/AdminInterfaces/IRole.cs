using API.Data.Entities;

namespace API.Models.IntAdmin.Interfaces
{

    public interface IRole
    {
        int RoleId { get; set; }
        string RoleName { get; set; }
        string RoleDescription { get; set; }
        string RoleStatus { get; set; }

        // CRUD methods
        Result<IRole> AddRole(IRole role);
        Result<IRole> UpdateRole(IRole role);
        Result<IRole> GetRoleById(int roleId);
        Result<List<IRole>> GetAllRoles();
        Result<bool> RemoveRole(int roleId);

        // Permission management methods
        Result<bool> AddPermissionToRole(IPermission permission);
        Result<bool> RemovePermissionFromRole(int permissionId);
    }
}
