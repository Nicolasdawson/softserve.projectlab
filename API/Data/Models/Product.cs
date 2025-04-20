using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Data.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [Required]
        [MaxLength(400)]
        public string Detail { get; set; }

        [MaxLength(100)]
        [Required]
        public string ImageUrl { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public bool IsTrending { get; set; } = false;

        [Required]
        public bool IsBestSelling { get; set; } = false;

        [Required]
        public bool Available { get; set; } = true;

        [Required]
        public int CategoryId { get; set; }

        [NotMapped]
        public IFormFile Image { get; set; }
        public ICollection<ShoppingCartItem> ShoppingCartItems { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
