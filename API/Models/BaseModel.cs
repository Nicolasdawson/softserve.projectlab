using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class BaseModel
    {
        [Column(TypeName = "datetime")]
        public string Created_at { get; set; }
        
        [Column(TypeName = "datetime")]
        public string Update_at { get; set; }
        
        [Column(TypeName = "datetime")]
        public string Deleted_at { get;  set; }

        [Column(TypeName = "bit")]
        public bool Is_active { get; set; }
    }
}
