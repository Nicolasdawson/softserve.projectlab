using System;
using System.Collections.Generic;

namespace API.Data.Entities;

public partial class CatalogEntity
{
    public int CatalogId { get; set; }

    public string CatalogName { get; set; } = null!;

    public string CatalogDescription { get; set; } = null!;

    public bool CatalogStatus { get; set; }

    public virtual ICollection<CatalogCategoryEntity> CatalogCategoryEntities { get; set; } = new List<CatalogCategoryEntity>();
}
