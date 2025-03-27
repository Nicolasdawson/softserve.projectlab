using System;
using System.Collections.Generic;

namespace API.Entities;

public partial class UserRole
{
    public int UserId { get; set; }

    public int RoleId { get; set; }

    public virtual Role Role { get; set; } = null!;

    public virtual RolePermission RoleNavigation { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
