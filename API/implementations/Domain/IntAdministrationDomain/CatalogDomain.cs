using API.Models;
using API.Models.IntAdmin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Implementations.Domain
{
    /// <summary>
    /// Domain class for handling Catalog operations.
    /// Uses in-memory storage for catalogs.
    /// </summary>
    public class CatalogDomain
    {
        // In-memory storage for catalogs
        private readonly List<Catalog> _catalogs = new List<Catalog>();

        /// <summary>
        /// Creates a new catalog and adds it to the in-memory list.
        /// </summary>
        /// <param name="catalog">Catalog object to be created</param>
        /// <returns>Result object containing the created catalog</returns>
        public async Task<Result<Catalog>> CreateCatalog(Catalog catalog)
        {
            try
            {
                _catalogs.Add(catalog);
                return Result<Catalog>.Success(catalog);
            }
            catch (Exception ex)
            {
                return Result<Catalog>.Failure($"Failed to create catalog: {ex.Message}");
            }
        }

        /// <summary>
        /// Updates an existing catalog if found.
        /// </summary>
        /// <param name="catalog">Catalog object with updated information</param>
        /// <returns>Result object containing the updated catalog</returns>
        public async Task<Result<Catalog>> UpdateCatalog(Catalog catalog)
        {
            try
            {
                var existingCatalog = _catalogs.FirstOrDefault(c => c.CatalogID == catalog.CatalogID);
                if (existingCatalog != null)
                {
                    existingCatalog.CatalogName = catalog.CatalogName;
                    existingCatalog.CatalogDescription = catalog.CatalogDescription;
                    existingCatalog.CatalogStatus = catalog.CatalogStatus;
                    existingCatalog.Categories = catalog.Categories;
                    return Result<Catalog>.Success(existingCatalog);
                }
                else
                {
                    return Result<Catalog>.Failure("Catalog not found.");
                }
            }
            catch (Exception ex)
            {
                return Result<Catalog>.Failure($"Failed to update catalog: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves a catalog by its unique ID.
        /// </summary>
        /// <param name="catalogId">Unique identifier of the catalog</param>
        /// <returns>Result object containing the catalog if found, otherwise an error</returns>
        public async Task<Result<Catalog>> GetCatalogById(int catalogId)
        {
            try
            {
                var catalog = _catalogs.FirstOrDefault(c => c.CatalogID == catalogId);
                return catalog != null
                    ? Result<Catalog>.Success(catalog)
                    : Result<Catalog>.Failure("Catalog not found.");
            }
            catch (Exception ex)
            {
                return Result<Catalog>.Failure($"Failed to get catalog: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves all catalogs stored in memory.
        /// </summary>
        /// <returns>Result object containing a list of catalogs</returns>
        public async Task<Result<List<Catalog>>> GetAllCatalogs()
        {
            try
            {
                return Result<List<Catalog>>.Success(_catalogs);
            }
            catch (Exception ex)
            {
                return Result<List<Catalog>>.Failure($"Failed to retrieve catalogs: {ex.Message}");
            }
        }

        /// <summary>
        /// Removes a catalog by its unique ID.
        /// </summary>
        /// <param name="catalogId">Unique identifier of the catalog to remove</param>
        /// <returns>Result object indicating success or failure</returns>
        public async Task<Result<bool>> RemoveCatalog(int catalogId)
        {
            try
            {
                var catalogToRemove = _catalogs.FirstOrDefault(c => c.CatalogID == catalogId);
                if (catalogToRemove != null)
                {
                    _catalogs.Remove(catalogToRemove);
                    return Result<bool>.Success(true);
                }
                else
                {
                    return Result<bool>.Failure("Catalog not found.");
                }
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure($"Failed to remove catalog: {ex.Message}");
            }
        }
    }
}
