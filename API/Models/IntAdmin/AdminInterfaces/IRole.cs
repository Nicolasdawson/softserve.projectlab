using API.Data.Entities;

namespace API.Models.IntAdmin.Interfaces
{

    public interface IRole
    {
        int RoleId { get; set; }
        string RoleName { get; set; }
        string RoleDescription { get; set; }
        bool RoleStatus { get; set; }
    }
}
