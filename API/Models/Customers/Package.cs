using API.Models.IntAdmin;

namespace API.Models.Customers;

public class Package
{
    public string Id { get; set; }

    public DateTime SaleDate { get; set; }

    public List<Item> Cart { get; set; } = new List<Item>();

    public Customer Customer { get; set; } = new Customer();
}