using System;
using System.Collections.Generic;

namespace API.Entities;

public partial class WarehouseItem
{
    public int WarehouseId { get; set; }

    public int Sku { get; set; }

    public int? Stock { get; set; }

    public virtual Item SkuNavigation { get; set; } = null!;

    public virtual Warehouse Warehouse { get; set; } = null!;
}
