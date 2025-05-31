using API.Models;

namespace API.Services
{
    public interface IProductImageService
    {
        Task<Boolean> CreateProductImageAsync(List<ProductImage> images);
        Task<Boolean> GetProductImagesByProductId(Guid id);
    }
}
