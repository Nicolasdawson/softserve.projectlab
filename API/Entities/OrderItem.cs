using System;
using System.Collections.Generic;

namespace API.Entities;

public partial class OrderItem
{
    public int OrderId { get; set; }

    public int Sku { get; set; }

    public int? Quantity { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual Item SkuNavigation { get; set; } = null!;
}
