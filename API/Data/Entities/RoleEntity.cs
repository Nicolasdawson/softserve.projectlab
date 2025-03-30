using System;
using System.Collections.Generic;

namespace API.Data.Entities;

public partial class RoleEntity
{
    public int RoleId { get; set; }

    public string? RoleName { get; set; }

    public string? RoleDescription { get; set; }

    public bool? RoleStatus { get; set; }

    public virtual ICollection<UserRoleEntity> UserRoleEntities { get; set; } = new List<UserRoleEntity>();
}
