using System;
using System.Collections.Generic;

namespace API.Data.Entities;

public partial class RolePermissionEntity
{
    public int RoleId { get; set; }

    public int PermissionId { get; set; }

    public virtual PermissionEntity Permission { get; set; } = null!;

    public virtual ICollection<UserRoleEntity> UserRoleEntities { get; set; } = new List<UserRoleEntity>();
}
