namespace Frontend.DTO;

public class ProductDetailDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public Guid CategoryId { get; set; }
    public string Description { get; set; } = default!;
    public List<string> ImageUrls { get; set; } = new();

    public decimal Price { get; set; }
    public int Stock { get; set; }
    public decimal Weight { get; set; }
    public decimal Height { get; set; }
    public decimal Width { get; set; }
    public decimal Length { get; set; }
    public string categoryName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
