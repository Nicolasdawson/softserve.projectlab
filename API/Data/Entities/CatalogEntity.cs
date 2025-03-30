using System;
using System.Collections.Generic;

namespace API.Data.Entities;

public partial class CatalogEntity
{
    public int CatalogId { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public bool? Status { get; set; }

    // Navigation property to related items
    public ICollection<CatalogCategoryEntity> CatalogCategories { get; set; }
}
