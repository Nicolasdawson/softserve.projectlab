using System;
using System.Collections.Generic;

namespace API.Entities;

public partial class CategoryItem
{
    public int CategoryId { get; set; }

    public int Sku { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual Item SkuNavigation { get; set; } = null!;
}
