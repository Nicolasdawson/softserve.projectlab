using System;
using System.Collections.Generic;

namespace API.Entities;

public partial class Branch
{
    public int BranchId { get; set; }

    public string? Name { get; set; }

    public string? City { get; set; }

    public string? Region { get; set; }

    public string? ContactNumber { get; set; }

    public string? ContactEmail { get; set; }

    public string? Address { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();

    public virtual ICollection<Warehouse> Warehouses { get; set; } = new List<Warehouse>();
}
