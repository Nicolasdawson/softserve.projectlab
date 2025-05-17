using softserve.projectlabs.Shared.Utilities;

namespace API.Models.IntAdmin.AdminInterfaces;

public interface IPermission
{
    int PermissionId { get; set; }
    string PermissionName { get; set; }
    string PermissionDescription { get; set; }
}
