using System.Text.Json;
using Microsoft.JSInterop;

namespace Frontend.Services;

public class CartService
{
    private const string CartKey = "shoppingCart";
    private readonly IJSRuntime _js;

    public CartService(IJSRuntime js)
    {
        _js = js;
    }

    public async Task<List<CartItemModel>> GetCartAsync()
    {
        var json = await _js.InvokeAsync<string>("localStorage.getItem", CartKey);
        return string.IsNullOrEmpty(json) ? new List<CartItemModel>() : JsonSerializer.Deserialize<List<CartItemModel>>(json)!;
    }

    public async Task AddToCartAsync(CartItemModel item)
    {
        var cart = await GetCartAsync();
        var existing = cart.FirstOrDefault(p => p.ProductId == item.ProductId);
        if (existing != null)
        {
            existing.Quantity += item.Quantity;
        }
        else
        {
            cart.Add(item);
        }
        await _js.InvokeVoidAsync("localStorage.setItem", CartKey, JsonSerializer.Serialize(cart));
    }

    public async Task<int> GetCartTotalItemsAsync()
    {
        var cart = await GetCartAsync();
        return cart.Sum(p => p.Quantity);
    }

    public async Task SaveCartAsync(List<CartItemModel> cart)
    {
        var json = JsonSerializer.Serialize(cart);
        await _js.InvokeVoidAsync("localStorage.setItem", CartKey, json);
    }
}
