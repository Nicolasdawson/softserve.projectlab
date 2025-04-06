namespace API.Models;

public class Category
{
    public string Name { get; set; } = default!;

    public List<Product> Products { get; set; } = new List<Product>();
}