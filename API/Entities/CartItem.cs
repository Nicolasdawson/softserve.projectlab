using System;
using System.Collections.Generic;

namespace API.Entities;

public partial class CartItem
{
    public int CartId { get; set; }

    public int Sku { get; set; }

    public virtual Cart Cart { get; set; } = null!;

    public virtual Item SkuNavigation { get; set; } = null!;
}
