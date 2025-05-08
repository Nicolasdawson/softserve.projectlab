using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace softserve.projectlabs.Shared.DTOs.User;

public class UserCreateDto
{
    public string UserFirstName { get; set; } = string.Empty;
    public string UserLastName { get; set; } = string.Empty;
    public string UserContactEmail { get; set; } = null!;
    public string UserContactNumber { get; set; } = null!;
    public string UserPassword { get; set; } = string.Empty; // Plain password
    public int BranchId { get; set; }

    public List<int> RoleIds { get; set; } = new List<int>();
}
