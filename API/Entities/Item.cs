using System;
using System.Collections.Generic;

namespace API.Entities;

public partial class Item
{
    public int Sku { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public int? OriginalStock { get; set; }

    public int? CurrentStock { get; set; }

    public string? Currency { get; set; }

    public decimal? UnitCost { get; set; }

    public decimal? MarginGain { get; set; }

    public decimal? Discount { get; set; }

    public decimal? AdditionalTax { get; set; }

    public decimal? ItemPrice { get; set; }

    public bool? ItemStatus { get; set; }

    public int? CategoryId { get; set; }

    public string? Image { get; set; }

    public virtual Category? Category { get; set; }
}
