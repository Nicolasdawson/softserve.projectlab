using System;
using System.Collections.Generic;

namespace API.Entities;

public partial class Catalog
{
    public int CatalogId { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public bool? Status { get; set; }
}
