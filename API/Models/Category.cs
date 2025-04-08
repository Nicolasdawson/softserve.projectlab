using API.Abstractions;

namespace API.Models;

public class Category : Base
{
    public string Name { get; set; } = default!;

    // One Catory has many Products
    public ICollection<Product> Products { get; set; } = new List<Product>();


}