using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using Frontend.DTO;

namespace Frontend.Services;

public class ProductService : IProductService
{
    private readonly HttpClient _httpClient;

    private JsonSerializerOptions _jsonDefaultOptions => new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true,
    };
    public ProductService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ProductModel[]?> GetProducts(int pageNumber, int pageSize)
    {
        string url = $"http://localhost:5262/api/product/pag?pageNumber={pageNumber}&pageSize={pageSize}";
        var response = await _httpClient.GetFromJsonAsync<ProductModel[]>(url);

        return response;        
    }

    public async Task<ProductDetailDTO?> GetById(Guid id)
    {
        string url = $"http://localhost:5262/api/product/{id}";
        var response = await _httpClient.GetFromJsonAsync<ProductDetailDTO>(url);
        return response;
    }
}

