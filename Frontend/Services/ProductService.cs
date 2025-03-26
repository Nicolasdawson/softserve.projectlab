using System.Net.Http;
using System.Net.Http.Json;

namespace Frontend.Services;

public class ProductService
{
    private readonly HttpClient _httpClient;

    public ProductService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public Task<ProductModel[]?> GetProducts()
    {
        var data = _httpClient.GetFromJsonAsync<ProductModel[]>("http://localhost:5262/api/product/");
        Console.WriteLine(data);
        return data;
    }
}

