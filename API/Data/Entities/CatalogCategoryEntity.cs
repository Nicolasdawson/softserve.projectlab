using System;
using System.Collections.Generic;

namespace API.Data.Entities;

public class CatalogCategoryEntity
{
    public int CatalogId { get; set; }  // Foreign key to CatalogEntity
    public int CategoryId { get; set; }  // Foreign key to CategoryEntity

    // Navigation properties to related entities
    public CatalogEntity Catalog { get; set; }
    public CategoryEntity Category { get; set; }
}



