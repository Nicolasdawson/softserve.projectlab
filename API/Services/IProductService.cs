using API.DTO;
using API.Models;
namespace API.Services;

public interface IProductService
{
    Task<Product> CreateProductAsync(Product product);
    Task<IEnumerable<ProductWithImagesDTO>> GetAllProductsPaged(int pageNumber, int pageSize);
    Task<ProductDetailWithImages?> GetProductDetailById(Guid id);
    Task<bool> DeleteProduct(Guid id);
    Task<bool> UpdateProduct(Guid id, Product updatedProduct);
    IEnumerable<Product> GetProductsByCategory(Guid category);
    Task<ActionResponseDTO<int>> GetTotalRecordsAsync(PaginationDTO pagination);
}
