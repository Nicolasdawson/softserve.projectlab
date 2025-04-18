using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Data.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [Required]
        [MaxLength(200)]
        public string Email { get; set; }

        [Required]
        [MaxLength(200)]
        public string Password { get; set; }

        [Required]
        [MaxLength(10)]
        public string Role { get; set; } = "User";

        public ICollection<ShoppingCartItem> shoppingCartItems { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
