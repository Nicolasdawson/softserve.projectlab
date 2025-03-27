using System;
using System.Collections.Generic;

namespace API.Entities;

public partial class Category
{
    public int CategoryId { get; set; }

    public string? Name { get; set; }

    public bool? Status { get; set; }

    public virtual ICollection<Item> Items { get; set; } = new List<Item>();
}
