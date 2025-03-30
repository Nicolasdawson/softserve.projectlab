using System;
using System.Collections.Generic;

namespace API.Data.Entities;

public partial class PermissionEntity
{
    public int PermissionId { get; set; }

    public string? PermissionName { get; set; }

    public string? PermissionDescription { get; set; }

    public virtual ICollection<RolePermissionEntity> RolePermissionEntities { get; set; } = new List<RolePermissionEntity>();
}
