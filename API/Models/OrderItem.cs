namespace API.Models;

public class OrderItem
{
    public Guid Id { get; set; }
    public Guid IdOrder { get; set; }
    public Guid IdProduct { get; set; }

    public int Quantity { get; set; }
    public decimal Price { get; set; } // Precio del producto al momento de la orden

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    // Relaciones
    public Order Order { get; set; }
    public Product Product { get; set; }
}
