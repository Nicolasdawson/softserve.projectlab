using API.Data.Entities;
using softserve.projectlabs.Shared.Utilities;

namespace API.Models.IntAdmin.AdminInterfaces;

public interface IUser
{
    int UserId { get; set; }
    string UserFirstName { get; set; }
    string UserLastName { get; set; }
    string UserContactEmail { get; set; }
    string UserContactNumber { get; set; }
    byte[] PasswordHash { get; set; }
    byte[] PasswordSalt { get; set; }
    bool UserStatus { get; set; }
    string UserImage { get; set; }
    int BranchId { get; set; }

    List<IRole> Roles { get; set; }
}
