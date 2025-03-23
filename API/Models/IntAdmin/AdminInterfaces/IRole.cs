namespace API.Models.IntAdmin.Interfaces
{
    public interface IRole
    {
        int RoleId { get; set; }
        string RoleName { get; set; }
        string Description { get; set; }
        string Status { get; set; }

        Result<IRole> AddRole(IRole role);
        Result<IRole> UpdateRole(IRole role);
        Result<IRole> GetRoleById(int roleId);
        Result<List<IRole>> GetAllRoles();
        Result<bool> RemoveRole(int roleId);
    }
}
