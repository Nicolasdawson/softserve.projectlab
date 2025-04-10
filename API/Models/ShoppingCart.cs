using System.ComponentModel.DataAnnotations;
using API.Abstractions;

namespace API.Models
{
    public class ShoppingCart : Base
    {
        public int Quantity { get; set; }

        // ForeignKey: IdProduct, IdOrder
        public Guid IdProduct { get; set; }
        public Guid IdOrder { get; set; }

        // Navigation Properties
        public Product Product { get; set; } = default!;
        public Order Order { get; set; } = default!;
    }
}
