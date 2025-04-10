using API.Abstractions;

namespace API.Models
{
    public class City : Base
    {
        public string Name { get; set; } = default!;
        public string PostalCode { get; set; } = default!;

        // Foreign Key: IdRegion
        public Guid IdRegion { get; set; }

        // Navigation Property
        public Region Region { get; set; } = default!;
        
        // One City has many DeliveryAddress
        public ICollection<DeliveryAddress> DeliveryAddresses { get; set; } = new List<DeliveryAddress>();
    }
}
