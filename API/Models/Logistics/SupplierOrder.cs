using API.Models.IntAdmin;
using System;
using System.Collections.Generic;

namespace Logistics.Models
{
    public class SupplierOrder
    {
        public int OrderId { get; set; }
        public int SupplierId { get; set; }
        public List<Item> OrderedItems { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? ExpectedDeliveryDate { get; set; }
        public string Status { get; set; } // Example: "Pending", "Shipped", "Delivered"

        public SupplierOrder(int supplierId, List<Item> orderedItems)
        {
            OrderId = new Random().Next(1000, 9999); // Temporary ID generation
            SupplierId = supplierId;
            OrderedItems = orderedItems;
            OrderDate = DateTime.UtcNow;
            Status = "Pending";
        }

        public void UpdateStatus(string newStatus)
        {
            Status = newStatus;
        }
    }
}
