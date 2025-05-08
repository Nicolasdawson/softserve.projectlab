namespace Frontend.Models;

public class ProductModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public Guid CategoryId { get; set; }
    public string Description { get; set; } = default!;
    public List<string> ImageUrls { get; set; } = new();
    
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
public record GetProductsResponse(IEnumerable<ProductModel> Products);

