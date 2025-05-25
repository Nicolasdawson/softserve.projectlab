using Frontend.Services;
using Frontend.Shared;
using Microsoft.AspNetCore.Components;

namespace Frontend.Pages.ShoppingCart;

public partial class ShoppingCart
{
    [Inject] private CartService CartService { get; set; } = default!;
    [Inject] private CartState CartState { get; set; } = default!;

    private List<CartItemModel> CartItems { get; set; } = new();
    private string _promoCode = string.Empty;
    protected override async Task OnInitializedAsync()
    {
        CartState.OnChange += StateHasChanged;
        await LoadCartItemsAsync();
    }
    private async Task LoadCartItemsAsync()
    {
        CartItems = await CartService.GetCartAsync();
    }

    private async Task UpdateQuantity(CartItemModel item, int delta)
    {
        item.Quantity = Math.Max(1, item.Quantity + delta);
        await SaveCartAsync();
    }

    private async Task RemoveItem(CartItemModel item)
    {
        CartItems.Remove(item);
        await SaveCartAsync();
    }

    private async Task SaveCartAsync()
    {
        await CartService.SaveCartAsync(CartItems);
        CartState.NotifyCartChanged();
        await LoadCartItemsAsync(); // recargar después de guardar
    }

    private decimal Subtotal => CartItems.Sum(i => i.Price * i.Quantity);
    private decimal shipping = 0;
    private decimal Tax => Subtotal * 0.1m;
    private decimal Total => Subtotal + Tax + shipping;

    public void Dispose()
    {
        CartState.OnChange -= StateHasChanged;
    }

}
