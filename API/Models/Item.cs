namespace API.Models;

public class Item
{
    public int Id { get; set; }
    public string Sku { get; set; }
    
    public decimal Price { get; set; }
    
    public decimal Discount { get; set; }
}