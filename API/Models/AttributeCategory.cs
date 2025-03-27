using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("attribute_category")]
    public class AttributeCategory
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(255)]
        public string Name { get; set; }
    }
}
