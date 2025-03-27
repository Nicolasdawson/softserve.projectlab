using API.Models;

namespace API.implementations.Domain
{
    public interface IProductProcessor
    {
        List<Product> GetAllProducts(bool? isActive);
        Product? GetProductByID(int id);
        Product? AddProduct(Product obj);
        Product? UpdateProduct(int id, Product obj);
        bool DeleteProduct(int id);
    }
}
