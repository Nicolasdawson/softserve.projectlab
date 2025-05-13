using API.Abstractions;

namespace API.Models
{
    public class Order : Base
    {
        public int IdCustomer { get; set; }
        public Guid IdDeliveryAddress { get; set; }
        public Guid IdPayment { get; set; }
        public string OrderNumber { get; set; } = default!;
        public string Status { get; set; } = default!;
        public decimal TotalPrice { get; set; }

        // Navigation Properties
        public Customer Customer { get; set; } = default!;
        public DeliveryAddress DeliveryAddress { get; set; } = default!;
        public Payment Payment { get; set; } = default!;
        // One Order has many ShoppingCarts
        public ICollection<ShoppingCart> DeliveryAddresses { get; set; } = new List<ShoppingCart>();
    }
}
