using System.ComponentModel.DataAnnotations.Schema;
using API.Abstractions;

namespace API.Models
{
    public class DeliveryAddress : Base
    {
        public string StreetName { get; set; } = default!;
        public string StreetNumber { get; set; } = default!;
        public string? StreetNameOptional { get; set; } = default!;
        public bool IsDeleted { get; set; } = false;

        // Foreign Key: IdCity
        [ForeignKey("IdCity")]
        public Guid IdCity { get; set; }

        // Navigation Property
        public City City { get; set; } = default!;
    }
}
