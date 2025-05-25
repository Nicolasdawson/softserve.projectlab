using Frontend.DTO;

namespace Frontend.Services;

public interface IProductService
{
    Task<ProductModel[]?> GetProducts(int pageNumber, int pageSize);
    Task<ProductDetailDTO> GetById(Guid id);
}
