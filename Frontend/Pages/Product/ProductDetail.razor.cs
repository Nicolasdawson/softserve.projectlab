using System.Text.Json;
using Microsoft.AspNetCore.Components;
using Frontend.DTO;
using Frontend.Shared;

namespace Frontend.Pages.Product;

public partial class ProductDetail
{
    [Parameter]
    public string Id { get; set; } = string.Empty;

    private List<string>? Images;
    private string? SelectedImage;
    //private string _selectedColor = "Black";
    private int _quantity;

    private ProductDetailDTO Product;

    protected override async Task OnInitializedAsync()
    {
        Console.WriteLine($"Received Id: {Id}");

        if (Guid.TryParse(Id, out var parsedId))
        {
            Product = await ProductService.GetById(parsedId);
            Console.WriteLine(JsonSerializer.Serialize(Product, new JsonSerializerOptions
            {
                WriteIndented = true
            }));
            Images = Product.ImageUrls?.Take(3).ToList() ?? new List<string>();
            SelectedImage = Images.FirstOrDefault() ?? "images/placeholder.jpg";
        }
        else
        {
            Console.WriteLine("Id inválido");
        }
    }
    private void ChangeImage(string newImage)
    {
        SelectedImage = newImage;
    }

    private async Task AddToCart(ProductDetailDTO product)
    {
        var item = new CartItemModel
        {
            ProductId = product.Id,
            Name = product.Name,
            Price = product.Price,
            Quantity = _quantity
        };

        await CartService.AddToCartAsync(item);
        CartState.NotifyCartChanged();
    }
}
