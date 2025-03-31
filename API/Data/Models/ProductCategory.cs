using System;
using System.Collections.Generic;

namespace API.Data.Models;

public partial class ProductCategory
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdateAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
