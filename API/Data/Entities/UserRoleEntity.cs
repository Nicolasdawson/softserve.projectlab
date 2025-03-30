using System;
using System.Collections.Generic;

namespace API.Data.Entities;

public partial class UserRoleEntity
{
    public int UserId { get; set; }

    public int RoleId { get; set; }

    public virtual RoleEntity Role { get; set; } = null!;

    public virtual RolePermissionEntity RoleNavigation { get; set; } = null!;

    public virtual UsersEntity User { get; set; } = null!;
}
