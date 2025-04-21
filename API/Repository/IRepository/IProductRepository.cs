using API.Data.Models;
using API.Data.Models.DTOs.Product;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Repository.IRepository
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProductByName(string name);
        Task<IEnumerable<ProductGetDto>> GetProducts(string? productType = null, int? categoryId = null);
        Task<bool> AddProduct(Product product);
        Task<bool> UpdateProduct(Product product);
        Task<bool> DeleteProduct(int id);
        Product GetProduct(int productId);
    }
}
