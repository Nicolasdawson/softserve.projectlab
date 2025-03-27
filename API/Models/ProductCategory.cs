using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("product_category")]
    public class ProductCategory : BaseModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
    }
}
