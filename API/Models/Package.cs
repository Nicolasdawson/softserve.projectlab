namespace API.Models;

public class Package
{
    public string Id { get; set; }
    
    public DateTime SaleDate { get; set; }

    public List<Item> Cart { get; set; } = new List<Item>();

    public Customer Customer { get; set; } = new Customer();
}