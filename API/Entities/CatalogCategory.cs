using System;
using System.Collections.Generic;

namespace API.Entities;

public partial class CatalogCategory
{
    public int CatalogId { get; set; }

    public int CategoryId { get; set; }

    public virtual Catalog Catalog { get; set; } = null!;

    public virtual Category Category { get; set; } = null!;
}
