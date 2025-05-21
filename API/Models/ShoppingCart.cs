using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models;

public class ShoppingCart
{
    public Guid Id { get; set; }

    [Required]
    public int IdCustomer { get; set; }

    [Required]
    public Guid IdProduct { get; set; }

    [Required]
    [Range(1, int.MaxValue)]
    public int Quantity { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Relaciones de navegación (opcionales, pero útiles)
    public Customer? Customer { get; set; }
    public Product? Product { get; set; }
}
