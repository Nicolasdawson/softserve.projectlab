using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Data.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        public ICollection<Product> Products { get; set; }

    }
}
