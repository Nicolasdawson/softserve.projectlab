using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("attribute")]
    public class Attribute : BaseModel
    {
        [Key]
        public int Id { get; set; }
        public int Id_product { get; set; }
        public int Id_attribute_category { get; set; }
        [MaxLength(255)]
        public string Field { get; set; }
        [MaxLength(255)]
        public string Value { get; set; }
    }
}
