using API.Data.Entities;
using API.Models.IntAdmin.AdminInterfaces;
using softserve.projectlabs.Shared.Utilities;

namespace API.Models.IntAdmin;

public class User : IUser
{
    public int UserId { get; set; }
    public string UserFirstName { get; set; } = string.Empty;
    public string UserLastName { get; set; } = string.Empty;
    public string UserContactEmail { get; set; } = null!;
    public string UserContactNumber { get; set; } = string.Empty;
    public byte[] PasswordHash { get; set; } = null!;
    public byte[] PasswordSalt { get; set; } = null!;
    public bool UserStatus { get; set; }
    public string UserImage { get; set; } = string.Empty;
    public int BranchId { get; set; }

    // To adding roles to the user
    public List<IRole> Roles { get; set; } = new List<IRole>();
}
