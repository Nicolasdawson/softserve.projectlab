using System;
using System.Collections.Generic;

namespace API.Data.Entities;

public partial class BranchEntity
{
    public int BranchId { get; set; }

    public string? Name { get; set; }

    public string? City { get; set; }

    public string? Region { get; set; }

    public string? ContactNumber { get; set; }

    public string? ContactEmail { get; set; }

    public string? Address { get; set; }

    public virtual ICollection<UsersEntity> UsersEntities { get; set; } = new List<UsersEntity>();

    public virtual ICollection<WarehouseEntity> WarehouseEntities { get; set; } = new List<WarehouseEntity>();
}
