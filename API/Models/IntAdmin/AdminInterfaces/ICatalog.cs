using softserve.projectlabs.Shared.Utilities;

namespace API.Models.IntAdmin.AdminInterfaces;

public interface ICatalog
{
    int CatalogID { get; set; }
    string CatalogName { get; set; }
    string CatalogDescription { get; set; }
    bool CatalogStatus { get; set; }
}
