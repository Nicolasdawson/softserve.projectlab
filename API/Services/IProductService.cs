using API.DTO;
using API.Models;
namespace API.Services;

public interface IProductService
{
    Task<Product> CreateProductAsync(Product product);
    Task<IEnumerable<ProductWithImagesDTO>> GetAllProductsPaged(int pageNumber, int pageSize);
    Product? GetProductById(Guid id);
    bool DeleteProduct(Guid id);
    bool UpdateProduct(Guid id, Product updatedProduct);
    IEnumerable<Product> GetProductsByCategory(Guid category);
}
