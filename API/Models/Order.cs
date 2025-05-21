using API.Abstractions;

namespace API.Models;

    public class Order: Base
    {
        public Guid Id { get; set; }
        public int IdCustomer { get; set; }
        public Guid IdDeliveryAddress { get; set; }
        public Guid? IdPayment { get; set; }

        public string OrderNumber { get; set; }
        public string Status { get; set; } // created, paid, shipped, delivered, canceled, etc.
        public decimal TotalPrice { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Relaciones
        public Customer Customer { get; set; }
        public DeliveryAddress DeliveryAddress { get; set; }
        public Payment Payment { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }


