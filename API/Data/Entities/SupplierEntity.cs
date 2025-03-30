using System;
using System.Collections.Generic;

namespace API.Data.Entities;

public partial class SupplierEntity
{
    public int SupplierId { get; set; }

    public string? Name { get; set; }

    public string? SupplierAddress { get; set; }
}
