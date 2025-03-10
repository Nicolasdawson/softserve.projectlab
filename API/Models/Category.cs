namespace API.Models;

public class Category
{
    public string Name { get; set; }
    
    public List<Item> Items { get; set; } = new List<Item>();
}