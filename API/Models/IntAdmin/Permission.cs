using API.Models.IntAdmin.AdminInterfaces;
using softserve.projectlabs.Shared.Utilities;

namespace API.Models.IntAdmin;


public class Permission : IPermission
{

    public int PermissionId { get; set; }
    public string PermissionName { get; set; } = string.Empty;
    public string PermissionDescription { get; set; } = string.Empty;

}
