using System.Collections.Generic;

namespace API.Models.IntAdmin.Interfaces
{
    public interface ICatalog
    {
        int CatalogID { get; set; }

        string CatalogName { get; set; }

        string CatalogDescription { get; set; }

        bool CatalogStatus { get; set; }

        List<ICategory> Categories { get; set; }

        // CRUD methods

        /// <summary>
        /// Adds a new catalog.
        /// </summary>
        /// <param name="catalog">Catalog object to add</param>
        /// <returns>Result containing the added catalog</returns>
        Result<ICatalog> AddCatalog(ICatalog catalog);

        /// <summary>
        /// Updates an existing catalog.
        /// </summary>
        /// <param name="catalog">Catalog object with updated data</param>
        /// <returns>Result containing the updated catalog</returns>
        Result<ICatalog> UpdateCatalog(ICatalog catalog);

        /// <summary>
        /// Retrieves a catalog by its ID.
        /// </summary>
        /// <param name="catalogId">Catalog ID</param>
        /// <returns>Result containing the requested catalog</returns>
        Result<ICatalog> GetCatalogById(int catalogId);

        /// <summary>
        /// Retrieves all catalogs.
        /// </summary>
        /// <returns>Result containing a list of catalogs</returns>
        Result<List<ICatalog>> GetAllCatalogs();

        /// <summary>
        /// Removes a catalog by its ID.
        /// </summary>
        /// <param name="catalogId">Catalog ID</param>
        /// <returns>Result indicating success or failure</returns>
        Result<bool> RemoveCatalog(int catalogId);

        // Methods from the diagram to manage categories inside this catalog

        /// <summary>
        /// Adds a category to the catalog's Categories list.
        /// </summary>
        /// <param name="category">Category to add</param>
        /// <returns>Result indicating success or failure</returns>
        Result<bool> AddCategory();

        /// <summary>
        /// Removes a category from the catalog's Categories list by ID.
        /// </summary>
        /// <param name="categoryId">ID of the category to remove</param>
        /// <returns>Result indicating success or failure</returns>
        Result<bool> RemoveCategory();
    }
}
