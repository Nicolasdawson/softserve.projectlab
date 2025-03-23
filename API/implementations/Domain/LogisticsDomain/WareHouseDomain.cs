using API.Models.IntAdmin;
using API.Models;
using API.Models.Logistics.Interfaces;

namespace API.implementations.Domain
{
    public class WarehouseDomain
    {
        private readonly IWarehouse _warehouse;

        public WarehouseDomain(IWarehouse warehouse)
        {
            _warehouse = warehouse;
        }

        // Logic for adding an item to the warehouse
        public Result<IWarehouse> AddItem(Item item)
        {
            try
            {
                _warehouse.AddItem(item);
                return Result<IWarehouse>.Success(_warehouse);
            }
            catch (Exception ex)
            {
                return Result<IWarehouse>.Failure($"Error adding item: {ex.Message}");
            }
        }

        // Logic for removing an item from the warehouse
        public Result<IWarehouse> RemoveItem(Item item)
        {
            try
            {
                _warehouse.RemoveItem(item);
                return Result<IWarehouse>.Success(_warehouse);
            }
            catch (Exception ex)
            {
                return Result<IWarehouse>.Failure($"Error removing item: {ex.Message}");
            }
        }

        // Logic for getting available stock for a specific SKU
        public Result<IWarehouse> GetAvailableStock(string sku)
        {
            try
            {
                var stock = _warehouse.GetAvailableStock(sku);
                return Result<IWarehouse>.Success(_warehouse); // Or return `stock` instead
            }
            catch (Exception ex)
            {
                return Result<IWarehouse>.Failure($"Error getting available stock: {ex.Message}");
            }
        }
    }
}
