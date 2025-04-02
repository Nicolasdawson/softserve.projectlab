using System;
using System.Collections.Generic;

namespace API.Data.Entities;

public partial class CategoryItemEntity
{
    public int CategoryId { get; set; }
    public int Sku { get; set; }

    public virtual CategoryEntity Category { get; set; } = null!;
    public virtual ItemEntity Item { get; set; } = null!;
}



