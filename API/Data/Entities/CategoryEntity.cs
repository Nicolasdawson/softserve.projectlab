using System;
using System.Collections.Generic;

namespace API.Data.Entities;

public partial class CategoryEntity
{
    public int CategoryId { get; set; }

    public string? Name { get; set; }

    public bool? Status { get; set; }

    public virtual ICollection<ItemEntity> ItemEntities { get; set; } = new List<ItemEntity>();
}
