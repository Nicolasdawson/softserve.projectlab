using API.Data.Entities;
using API.Models;
using API.Models.IntAdmin;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Services.IntAdmin
{
    /// <summary>
    /// Service interface for catalog operations.
    /// </summary>
    public interface ICatalogService
    {
        /// <summary>
        /// Asynchronously adds a new catalog.
        /// </summary>
        Task<Result<Catalog>> AddCatalogAsync(Catalog catalog);

        /// <summary>
        /// Asynchronously updates an existing catalog.
        /// </summary>
        Task<Result<Catalog>> UpdateCatalogAsync(Catalog catalog);

        /// <summary>
        /// Asynchronously retrieves a catalog by its unique ID.
        /// </summary>
        Task<Result<Catalog>> GetCatalogByIdAsync(int catalogId);

        /// <summary>
        /// Asynchronously retrieves all catalogs.
        /// </summary>
        Task<Result<List<Catalog>>> GetAllCatalogsAsync();

        /// <summary>
        /// Asynchronously removes a catalog by its unique ID.
        /// </summary>
        Task<Result<bool>> RemoveCatalogAsync(int catalogId);
    }
}
