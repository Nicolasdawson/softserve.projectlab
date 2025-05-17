using API.Models.IntAdmin;
using softserve.projectlabs.Shared.DTOs.Catalog;
using softserve.projectlabs.Shared.Utilities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Services.IntAdmin;

public interface ICatalogService
{
    Task<Result<Catalog>> CreateCatalogAsync(CatalogCreateDto catalogDto);
    Task<Result<Catalog>> UpdateCatalogAsync(int catalogId, CatalogUpdateDto catalogDto);
    Task<Result<Catalog>> GetCatalogByIdAsync(int catalogId);
    Task<Result<List<Catalog>>> GetAllCatalogsAsync();
    Task<Result<bool>> DeleteCatalogAsync(int catalogId);

    // Category management
    Task<Result<bool>> AddCategoriesToCatalogAsync(int catalogId, List<int> categoryIds);
    Task<Result<bool>> RemoveCategoryFromCatalogAsync(int catalogId, int categoryId);
}
