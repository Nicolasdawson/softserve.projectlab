namespace API.DTO;

public class ProductDetailWithImages
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public decimal Weight { get; set; }
    public decimal Height { get; set; }
    public decimal Width { get; set; }
    public decimal Length { get; set; }
    public string categoryName { get; set; } = string.Empty;
    public List<string> ImageUrls { get; set; } = new();
}
