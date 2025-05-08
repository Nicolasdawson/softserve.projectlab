using API.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Data.Repositories.IntAdministrationRepository.Interfaces;

public interface ICatalogRepository
{
    Task<CatalogEntity> GetByIdAsync(int catalogId);
    Task<List<CatalogEntity>> GetAllAsync();
    Task<CatalogEntity> AddAsync(CatalogEntity entity);
    Task<CatalogEntity> UpdateAsync(CatalogEntity entity);
    Task DeleteAsync(int catalogId);
    Task<bool> ExistsAsync(int catalogId);

    // Métodos para la tabla pivote
    Task<List<CatalogCategoryEntity>> GetCatalogCategoriesAsync(int catalogId);
    Task AddCatalogCategoryAsync(CatalogCategoryEntity pivot);
    Task RemoveCatalogCategoryAsync(CatalogCategoryEntity pivot);
}
