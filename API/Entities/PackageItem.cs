using System;
using System.Collections.Generic;

namespace API.Entities;

public partial class PackageItem
{
    public int PackageId { get; set; }

    public int Sku { get; set; }

    public virtual Package Package { get; set; } = null!;

    public virtual Item SkuNavigation { get; set; } = null!;
}
