using System;
using System.Collections.Generic;

namespace API.Data.Entities;

public partial class WarehouseEntity
{
    public int WarehouseId { get; set; }

    public string? Location { get; set; }

    public int? Capacity { get; set; }

    public int? BranchId { get; set; }

    public virtual BranchEntity? Branch { get; set; }
}
