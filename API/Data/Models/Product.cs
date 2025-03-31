using System;
using System.Collections.Generic;

namespace API.Data.Models;

public partial class Product
{
    public int Id { get; set; }

    public string? ProductType { get; set; }

    public int? ProductCategory { get; set; }

    public int? Stock { get; set; }

    public double? Price { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdateAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<Attribute> Attributes { get; set; } = new List<Attribute>();

    public virtual ProductCategory? ProductCategoryNavigation { get; set; }
}
