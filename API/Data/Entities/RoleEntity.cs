using System;
using System.Collections.Generic;

namespace API.Data.Entities;

public partial class RoleEntity : BaseEntity
{
    public int RoleId { get; set; }

    public string RoleName { get; set; } = null!;

    public string RoleDescription { get; set; } = null!;

    public bool RoleStatus { get; set; }

    public virtual ICollection<RolePermissionEntity> RolePermissionEntities { get; set; } = new List<RolePermissionEntity>();

    public virtual ICollection<UserRoleEntity> UserRoleEntities { get; set; } = new List<UserRoleEntity>();
}
