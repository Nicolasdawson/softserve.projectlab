using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("product")]
    public class Product : BaseModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Product_type { get; set; }
        public int Product_category { get; set; }
        public int Stock { get; set; }
        public float Price { get; set; }

    }
}
