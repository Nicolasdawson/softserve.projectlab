using API.Models.IntAdmin;
using API.Models;
using API.Models.Logistics.Interfaces;

namespace Logistics.Models
{
    public class Supplier : ISupplier
    {
        public int SupplierId { get; set; }
        public string Name { get; set; }
        public string ContactInfo { get; set; }
        public string Address { get; set; }
        public List<Item> ProductsSupplied { get; set; }
        public bool IsActive { get; set; } // New field to track supplier availability
        public List<SupplierOrder> Orders { get; set; } // List of supplier orders

        public Supplier(int supplierId, string name, string contactInfo)
        {
            SupplierId = supplierId;
            Name = name;
            ContactInfo = contactInfo;
            ProductsSupplied = new List<Item>();
            Orders = new List<SupplierOrder>();
            IsActive = true;
        }

        public Result<ISupplier> AddSupplier(ISupplier supplier)
        {
            // Logic for adding a supplier
            return Result<ISupplier>.Success(supplier);
        }

        public Result<ISupplier> UpdateSupplier(ISupplier supplier)
        {
            // Logic to update an existing supplier
            return Result<ISupplier>.Success(supplier);
        }

        public Result<ISupplier> GetSupplierById(int supplierId)
        {
            var supplier = new Supplier(supplierId, "Sample Supplier", "contact@sample.com");
            return Result<ISupplier>.Success(supplier);
        }

        public Result<List<ISupplier>> GetAllSuppliers()
        {
            var suppliers = new List<ISupplier>
            {
                new Supplier(1, "Sample Supplier 1", "contact1@sample.com"),
                new Supplier(2, "Sample Supplier 2", "contact2@sample.com")
            };
            return Result<List<ISupplier>>.Success(suppliers);
        }

        public Result<bool> DeleteSupplier(int supplierId)
        {
            return Result<bool>.Success(true);
        }

        // NEW: Add a product to the supplier's inventory
        public Result<bool> AddProductToSupplier(Item item)
        {
            if (ProductsSupplied.Any(p => p.Sku == item.Sku))
                return Result<bool>.Failure("Product already supplied by this supplier.");

            ProductsSupplied.Add(item);
            return Result<bool>.Success(true);
        }

        // NEW: Remove a product from supplier's inventory
        public Result<bool> RemoveProductFromSupplier(Item item)
        {
            var existingItem = ProductsSupplied.FirstOrDefault(p => p.Sku == item.Sku);
            if (existingItem == null)
                return Result<bool>.Failure("Product not found in supplier inventory.");

            ProductsSupplied.Remove(existingItem);
            return Result<bool>.Success(true);
        }

        // NEW: Get all products a supplier provides
        public Result<List<Item>> GetSupplierProducts()
        {
            return Result<List<Item>>.Success(ProductsSupplied);
        }

        // NEW: Place an order with the supplier
        public Result<SupplierOrder> PlaceOrder(int supplierId, List<Item> items)
        {
            var order = new SupplierOrder(supplierId, items);
            Orders.Add(order);
            return Result<SupplierOrder>.Success(order);
        }

        // NEW: Get all orders placed with this supplier
        public Result<List<SupplierOrder>> GetSupplierOrders()
        {
            return Result<List<SupplierOrder>>.Success(Orders);
        }

        // NEW: Cancel a supplier order
        public Result<bool> CancelOrder(int orderId)
        {
            var order = Orders.FirstOrDefault(o => o.OrderId == orderId);
            if (order == null)
                return Result<bool>.Failure("Order not found.");

            Orders.Remove(order);
            return Result<bool>.Success(true);
        }

        // NEW: Check if the supplier is active
        public Result<bool> CheckSupplierAvailability()
        {
            return Result<bool>.Success(IsActive);
        }

        // NEW: Update supplier active/inactive status
        public Result<bool> UpdateSupplierStatus(bool isActive)
        {
            IsActive = isActive;
            return Result<bool>.Success(true);
        }

        // NEW: Rate a supplier and provide feedback
        public Result<bool> RateSupplier(int rating, string feedback)
        {
            // Save rating and feedback logic (could be stored in DB)
            return Result<bool>.Success(true);
        }

        // NEW: Get supplier rating
        public Result<int> GetSupplierRating()
        {
            // Return mock rating for now
            return Result<int>.Success(5); // Assume 5-star rating
        }
    }
}
