using API.Abstractions;

namespace API.Models;

public class Category : Base
{
    public string Name { get; set; } = default!;

    // One Category has many Products
    public ICollection<Product> Products { get; set; } = new List<Product>();


}