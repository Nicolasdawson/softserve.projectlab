namespace Frontend.Services;

public interface IProductService
{
    Task<ProductModel[]?> GetProducts(int pageNumber, int pageSize);
}
