using API.Implementations.Domain;
using API.Models;
using API.Models.IntAdmin;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Services.IntAdmin
{
    /// <summary>
    /// Service class for catalog operations. Delegates business logic to the CatalogDomain.
    /// </summary>
    public class CatalogService : ICatalogService
    {
        private readonly CatalogDomain _catalogDomain;

        /// <summary>
        /// Constructor with dependency injection for CatalogDomain.
        /// </summary>
        public CatalogService(CatalogDomain catalogDomain)
        {
            _catalogDomain = catalogDomain;
        }

        public async Task<Result<Catalog>> AddCatalogAsync(Catalog catalog)
        {
            return await _catalogDomain.CreateCatalog(catalog);
        }

        public async Task<Result<Catalog>> UpdateCatalogAsync(Catalog catalog)
        {
            return await _catalogDomain.UpdateCatalog(catalog);
        }

        public async Task<Result<Catalog>> GetCatalogByIdAsync(int catalogId)
        {
            return await _catalogDomain.GetCatalogById(catalogId);
        }

        public async Task<Result<List<Catalog>>> GetAllCatalogsAsync()
        {
            return await _catalogDomain.GetAllCatalogs();
        }

        public async Task<Result<bool>> RemoveCatalogAsync(int catalogId)
        {
            return await _catalogDomain.RemoveCatalog(catalogId);
        }
    }
}
