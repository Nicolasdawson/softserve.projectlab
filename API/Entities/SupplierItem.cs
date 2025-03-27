using System;
using System.Collections.Generic;

namespace API.Entities;

public partial class SupplierItem
{
    public int SupplierId { get; set; }

    public int Sku { get; set; }

    public virtual Item SkuNavigation { get; set; } = null!;

    public virtual Supplier Supplier { get; set; } = null!;
}
