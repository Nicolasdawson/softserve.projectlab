using System.ComponentModel.DataAnnotations;

namespace API.Data.Models
{
    public class ShoppingCartItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public int Qty { get; set; }

        [Required]
        public double TotalAmount { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public int UserId { get; set; }
    }
}
