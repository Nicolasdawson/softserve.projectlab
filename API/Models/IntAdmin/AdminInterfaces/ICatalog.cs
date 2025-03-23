using API.Models.IntAdmin.Interfaces;

namespace API.Models.IntAdmin.Interfaces
{
    public interface ICatalog
    {
        int CatalogId { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        string Status { get; set; }
        List<ICategory> Categories { get; set; }

        Result<ICatalog> AddCatalog(ICatalog catalog);
        Result<ICatalog> UpdateCatalog(ICatalog catalog);
        Result<ICatalog> GetCatalogById(int catalogId);
        Result<List<ICatalog>> GetAllCatalogs();
        Result<bool> RemoveCatalog(int catalogId);
    }
}
