namespace API.Models.IntAdmin;

public class Category
{
    public string Name { get; set; }

    public List<Item> Items { get; set; } = new List<Item>();
}