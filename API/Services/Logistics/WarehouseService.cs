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
                _warehouse.AddItem(item);
                return Result<IWarehouse>.Success(_warehouse);
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
                _warehouse.RemoveItem(item);
                return Result<IWarehouse>.Success(_warehouse);
            }
            catch (Exception ex)
            {
                return Result<IWarehouse>.Failure($"Failed to remove item: {ex.Message}");
            }
        }

        public Result<IWarehouse> GetAvailableStock(string sku)
        {
            try
            {
                _warehouse.GetAvailableStock(sku);
                return Result<IWarehouse>.Success(_warehouse);
            }
            catch (Exception ex)
            {
                return Result<IWarehouse>.Failure($"Failed to get available stock: {ex.Message}");
            }
        }

    }
}
