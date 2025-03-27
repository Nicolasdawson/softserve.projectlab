using System;
using System.Collections.Generic;

namespace API.Entities;

public partial class Warehouse
{
    public int WarehouseId { get; set; }

    public string? Location { get; set; }

    public int? Capacity { get; set; }

    public int? BranchId { get; set; }

    public virtual Branch? Branch { get; set; }
}
