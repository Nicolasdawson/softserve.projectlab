using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace softserve.projectlabs.Shared.DTOs.Catalog;

public class CatalogUpdateDto
{
    public string CatalogName { get; set; } = string.Empty;
    public string CatalogDescription { get; set; } = string.Empty;
    public bool CatalogStatus { get; set; }

    public List<int> CategoryIds { get; set; } = new();
}
