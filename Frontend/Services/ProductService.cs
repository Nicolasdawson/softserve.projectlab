using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;

namespace Frontend.Services;

public class ProductService
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
        string url = $"http://localhost:5262/api/product?pageNumber={pageNumber}&pageSize={pageSize}";
        var response = await _httpClient.GetFromJsonAsync<ProductModel[]>(url);

        return response;        
    }


}

