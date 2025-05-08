using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace softserve.projectlabs.Shared.DTOs.User;

public class UserUpdateDto
{
    public string UserFirstName { get; set; } = string.Empty;
    public string UserLastName { get; set; } = string.Empty;
    public string UserContactEmail { get; set; } = null!;
    public string UserContactNumber { get; set; } = null!;
    public string? UserPassword { get; set; } // null if not changing
    public bool UserStatus { get; set; }
    public int BranchId { get; set; }

    public List<int> RoleIds { get; set; } = new List<int>();
}
