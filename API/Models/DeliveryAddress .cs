using API.Abstractions;

namespace API.Models
{
    public class DeliveryAddress : Base
    {
        public string StreetName { get; set; } = default!;
        public string StreetNumber { get; set; } = default!;
        public string StreetNameOptional { get; set; } = default!;

        // Foreign Key: IdCity
        public Guid IdCity { get; set; }

        // Navigation Property
        public City City { get; set; } = default!;
    }
}
