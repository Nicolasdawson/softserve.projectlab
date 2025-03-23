using API.Models.IntAdmin;
using API.Models.Logistics.Interfaces;
using API.Models;
using API.Services.Logistics;

namespace API.Services.WareHouseService
{
    public class WarehouseService : IWarehouseService
    {
        private readonly IWarehouse _warehouse;

        // Constructor Injection - DI will inject the appropriate IWarehouse implementation
        public WarehouseService(IWarehouse warehouse)
        {
            _warehouse = warehouse;
        }

        // Business logic for adding an item to the warehouse
        public Result<IWarehouse> AddItem(Item item)
        {
            try
            {
                var result = _warehouse.AddItem(item); // Now it returns a Result<IWarehouse>
                return result; // Return the result
            }
            catch (Exception ex)
            {
                return Result<IWarehouse>.Failure($"Failed to add item: {ex.Message}");
            }
        }

        // Business logic for removing an item from the warehouse
        public Result<IWarehouse> RemoveItem(Item item)
        {
            try
            {
                var result = _warehouse.RemoveItem(item); // Now it returns a Result<IWarehouse>
                return result; // Return the result
            }
            catch (Exception ex)
            {
                return Result<IWarehouse>.Failure($"Failed to remove item: {ex.Message}");
            }
        }

        // Business logic for retrieving the available stock for a specific SKU
        public Result<IWarehouse> GetAvailableStock(string sku)
        {
            try
            {
                var result = _warehouse.GetAvailableStock(sku); // Assuming it now returns a Result<IWarehouse> or similar
                return result; // Return the result
            }
            catch (Exception ex)
            {
                return Result<IWarehouse>.Failure($"Failed to get available stock: {ex.Message}");
            }
        }
    }
}
