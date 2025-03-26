namespace Frontend.Services;

public interface IProductService
{
    Task<GetProductsResponse> GetProducts();
}
