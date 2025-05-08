using AutoMapper;
using API.Models.IntAdmin;
using softserve.projectlabs.Shared.Utilities;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Implementations.Domain;
using softserve.projectlabs.Shared.DTOs.Catalog;

namespace API.Services.IntAdmin
{
    public class CatalogService : ICatalogService
    {
        private readonly CatalogDomain _catalogDomain;
        private readonly IMapper _mapper;

        public CatalogService(CatalogDomain catalogDomain, IMapper mapper)
        {
            _catalogDomain = catalogDomain;
            _mapper = mapper;
        }

        public async Task<Result<Catalog>> CreateCatalogAsync(CatalogCreateDto catalogDto)
        {
            var domainModel = _mapper.Map<Catalog>(catalogDto); // ✅ DTO → Domain
            return await _catalogDomain.CreateCatalogAsync(domainModel);
        }

        public async Task<Result<Catalog>> UpdateCatalogAsync(int catalogId, CatalogUpdateDto catalogDto)
        {
            var domainModel = _mapper.Map<Catalog>(catalogDto);
            return await _catalogDomain.UpdateCatalogAsync(catalogId, domainModel);
        }

        public Task<Result<Catalog>> GetCatalogByIdAsync(int catalogId)
        {
            return _catalogDomain.GetCatalogByIdAsync(catalogId);
        }

        public Task<Result<List<Catalog>>> GetAllCatalogsAsync()
        {
            return _catalogDomain.GetAllCatalogsAsync();
        }

        public Task<Result<bool>> DeleteCatalogAsync(int catalogId)
        {
            return _catalogDomain.DeleteCatalogAsync(catalogId);
        }

        public Task<Result<bool>> AddCategoriesToCatalogAsync(int catalogId, List<int> categoryIds)
        {
            return _catalogDomain.AddCategoriesToCatalogAsync(catalogId, categoryIds);
        }

        public Task<Result<bool>> RemoveCategoryFromCatalogAsync(int catalogId, int categoryId)
        {
            return _catalogDomain.RemoveCategoryFromCatalogAsync(catalogId, categoryId);
        }
    }
}
