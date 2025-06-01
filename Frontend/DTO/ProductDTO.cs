using Microsoft.AspNetCore.Components.Forms;

namespace Frontend.DTO;

public class ProductDTO
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public decimal Price { get; set; }
    public decimal Weight { get; set; }
    public decimal Height { get; set; }
    public decimal Width { get; set; }
    public decimal Length { get; set; }
    public int Stock { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // ForeignKey: Idcategory
    public Guid IdCategory { get; set; }
    public List<IBrowserFile> Images { get; set; } = new();
}
