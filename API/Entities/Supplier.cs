using System;
using System.Collections.Generic;

namespace API.Entities;

public partial class Supplier
{
    public int SupplierId { get; set; }

    public string? Name { get; set; }

    public string? SupplierAddress { get; set; }
}
