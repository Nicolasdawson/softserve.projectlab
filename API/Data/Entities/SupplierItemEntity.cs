using System;
using System.Collections.Generic;

namespace API.Data.Entities;

public class SupplierItemEntity
{
    public int SupplierId { get; set; } // Foreign key to SupplierEntity
    public int Sku { get; set; } // Stock Keeping Unit
    public int? OrderId { get; set; } // Unique identifier for the order (nullable for catalog items)
    public DateTime? OrderDate { get; set; } // Date the order was placed
    public DateTime? ExpectedDeliveryDate { get; set; } // Expected delivery date
    public string Status { get; set; } // Status of the order (e.g., "Pending", "Completed")
    public bool IsOrder { get; set; } // Flag to differentiate between catalog and order items
    public int ItemQuantity { get; set; } // Quantity of the item

    // Navigation properties
    public SupplierEntity Supplier { get; set; }
    public ItemEntity SkuNavigation { get; set; } // Navigation property for ItemEntity
}


