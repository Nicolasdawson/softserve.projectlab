using System.ComponentModel.DataAnnotations;

namespace API.Data.Models
{
    public class OrderDetail
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int Qty { get; set; }

        [Required]
        public decimal TotalAmount { get; set; }

        [Required]
        public int OrderId { get; set; }

        [Required]
        public int ProductId { get; set; }
    }
}
