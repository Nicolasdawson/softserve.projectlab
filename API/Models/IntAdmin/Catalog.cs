using System.Collections.Generic;
using System.Linq;
using API.Data.Entities;
using API.Models.IntAdmin.Interfaces;

namespace API.Models.IntAdmin
{
    public class Catalog : ICatalog
    {

        public int CatalogID { get; set; }

        public string CatalogName { get; set; }

        public string CatalogDescription { get; set; }

        public bool CatalogStatus { get; set; }

        public List<ICategory> Categories { get; set; } = new List<ICategory>();

        public Catalog(int catalogID, string catalogName, string catalogDescription, bool catalogStatus, List<ICategory> categories)
        {
            CatalogID = catalogID;
            CatalogName = catalogName;
            CatalogDescription = catalogDescription;
            CatalogStatus = catalogStatus;
            Categories = categories;
        }

        public Catalog() { }

        /// <summary>
        /// Adds a new catalog.
        /// </summary>
        /// <param name="catalog">Catalog object to add</param>
        /// <returns>Result containing the added catalog</returns>
        public Result<ICatalog> AddCatalog(ICatalog catalog)
        {
            // TODO: Add logic to persist the new catalog (e.g., saving to a database)
            return Result<ICatalog>.Success(catalog);
        }

        /// <summary>
        /// Updates an existing catalog.
        /// </summary>
        /// <param name="catalog">Catalog object with updated data</param>
        /// <returns>Result containing the updated catalog</returns>
        public Result<ICatalog> UpdateCatalog(ICatalog catalog)
        {
            // TODO: Add logic to update the catalog in the data source
            return Result<ICatalog>.Success(catalog);
        }

        /// <summary>
        /// Retrieves a catalog by its ID.
        /// </summary>
        /// <param name="catalogId">Catalog ID</param>
        /// <returns>Result containing the requested catalog</returns>
        public Result<ICatalog> GetCatalogById(int catalogId)
        {
            // TODO: Fetch the catalog from the data source
            var exampleCatalog = new Catalog(
                catalogId,
                "Example Catalog",
                "Example catalog description",
                true,
                new List<ICategory>()
            );
            return Result<ICatalog>.Success(exampleCatalog);
        }

        /// <summary>
        /// Retrieves all catalogs.
        /// </summary>
        /// <returns>Result containing a list of catalogs</returns>
        public Result<List<ICatalog>> GetAllCatalogs()
        {
            // TODO: Fetch all catalogs from the data source
            var catalogs = new List<ICatalog>
            {
                new Catalog(1, "Catalog 1", "Description of catalog 1", true, new List<ICategory>()),
                new Catalog(2, "Catalog 2", "Description of catalog 2", false, new List<ICategory>())
            };
            return Result<List<ICatalog>>.Success(catalogs);
        }

        /// <summary>
        /// Removes a catalog by its ID.
        /// </summary>
        /// <param name="catalogId">Catalog ID</param>
        /// <returns>Result indicating success or failure</returns>
        public Result<bool> RemoveCatalog(int catalogId)
        {
            // TODO: Remove the catalog from the data source
            return Result<bool>.Success(true);
        }

        /// <summary>
        /// Adds a category to the catalog's Categories list.
        /// </summary>
        /// <returns>Result indicating success or failure</returns>
        public Result<bool> AddCategory()
        {
            // TODO: Add the logic to add a category to the list
            return Result<bool>.Success(true);
        }

        /// <summary>
        /// Removes a category from the catalog's Categories list by ID.
        /// </summary>
        /// <returns>Result indicating success or failure</returns>
        public Result<bool> RemoveCategory()
        {
            // TODO: Add the logic to remove a category from the list
            return Result<bool>.Success(true);
        }
    }
}
