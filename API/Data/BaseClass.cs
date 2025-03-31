using API.Data.Models;

namespace API.Data
{
    public class BaseClass
    {

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdateAt { get; set; }

        public DateTime? DeletedAt { get; set; }

        public bool? IsActive { get; set; }
    }
}
