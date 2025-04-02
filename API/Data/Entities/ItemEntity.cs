using System;
using System.Collections.Generic;

namespace API.Data.Entities;

public partial class ItemEntity
{
    public int Sku { get; set; }
    public string? Name { get; set; }
    public decimal? ItemPrice { get; set; }
    public string? Currency { get; set; }
    public decimal? AdditionalTax { get; set; }
    public decimal? Discount { get; set; }
    public decimal? UnitCost { get; set; }
    public decimal? MarginGain { get; set; }
    public string? Description { get; set; }
    public string? Image { get; set; }
    public int? OriginalStock { get; internal set; }
    public int? CurrentStock { get; internal set; }
    public bool? ItemStatus { get; internal set; }
    public int? CategoryId { get; internal set; }

    // Correct relationship with CategoryItemEntity
    public virtual ICollection<CategoryItemEntity> CategoryItems { get; set; } = new List<CategoryItemEntity>();
   
}




