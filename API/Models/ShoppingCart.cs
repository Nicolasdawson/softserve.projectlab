using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class ShoppingCart
    {
        [Key]  // Esto indica que 'Id' es la clave primaria
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    }
}
