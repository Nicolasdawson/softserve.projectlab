using System;
using System.Collections.Generic;

namespace API.Data.Entities;

public partial class CategoryEntity
{
    public int CategoryId { get; set; }
    public string? Name { get; set; }
    public bool? Status { get; set; }

    public virtual ICollection<CategoryItemEntity> CategoryItemEntities { get; set; } = new List<CategoryItemEntity>();
    public virtual ICollection<CatalogCategoryEntity> CatalogCategories { get; set; } = new List<CatalogCategoryEntity>();
}





