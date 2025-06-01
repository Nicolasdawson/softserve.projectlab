using System.Text.Json.Serialization;
using API.Abstractions;

namespace API.Models;

public class Category : Base
{
    public string Name { get; set; } = default!;

    // One Category has many Products
    [JsonIgnore]
    public ICollection<Product> Products { get; set; } = new List<Product>();


}