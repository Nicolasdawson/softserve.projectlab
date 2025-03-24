using System.Collections.Generic;
using System.Linq;
using API.Models;
using API.Models.IntAdmin;
using API.Models.Logistics;
using API.Models.Logistics.Interfaces;
using API.Services.Interfaces;

namespace API.Services
{
    public class WarehouseService : IWarehouseService
    {
        private readonly List<IWarehouse> _warehouses;
        
        public WarehouseService()
        {
            _warehouses = new List<IWarehouse>();
        }
        /// <summary>
        /// Get a warehouse by its ID
        /// </summary>
        /// <param name="warehouseId"></param>
        /// <returns></returns>
        private IWarehouse GetWarehouseById(int warehouseId)
        {
            return _warehouses.FirstOrDefault(w => w.WareHouseId == warehouseId);
        }
        /// <summary>
        /// Add an item to a warehouse  
        /// </summary>
        /// <param name="warehouseId"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public Result<IWarehouse> AddItemToWarehouse(int warehouseId, Item item)
        {
            var warehouse = GetWarehouseById(warehouseId);
            return warehouse != null ? warehouse.AddItem(item) : Result<IWarehouse>.Failure("Warehouse not found.");
        }
        /// <summary>
        /// Remove an item from a warehouse
        /// </summary>
        /// <param name="warehouseId"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public Result<IWarehouse> RemoveItemFromWarehouse(int warehouseId, Item item)
        {
            var warehouse = GetWarehouseById(warehouseId);
            return warehouse != null ? warehouse.RemoveItem(item) : Result<IWarehouse>.Failure("Warehouse not found.");
        }
        /// <summary>
        /// Check the stock of an item in a warehouse
        /// </summary>
        /// <param name="warehouseId"></param>
        /// <param name="sku"></param>
        /// <returns></returns>
        public Result<int> CheckWarehouseStock(int warehouseId, int sku)
        {
            var warehouse = GetWarehouseById(warehouseId);
            return warehouse != null ? warehouse.CheckItemStock(sku) : Result<int>.Failure("Warehouse not found.");
        }
        /// <summary>
        /// Transfer an item from one warehouse to another
        /// </summary>
        /// <param name="warehouseId"></param>
        /// <param name="sku"></param>
        /// <param name="quantity"></param>
        /// <param name="targetWarehouseId"></param>
        /// <returns></returns>
        public Result<bool> TransferItem(int warehouseId, int sku, int quantity, int targetWarehouseId)
        {
            var sourceWarehouse = GetWarehouseById(warehouseId);
            var targetWarehouse = GetWarehouseById(targetWarehouseId);

            if (sourceWarehouse == null || targetWarehouse == null)
                return Result<bool>.Failure("One or both warehouses not found.");

            return sourceWarehouse.TransferItem(sku, quantity, targetWarehouse);
        }
        /// <summary>
        /// Get a list of items with stock below a certain threshold
        /// </summary>
        /// <param name="warehouseId"></param>
        /// <param name="threshold"></param>
        /// <returns></returns>
        public Result<List<Item>> GetLowStockItems(int warehouseId, int threshold)
        {
            var warehouse = GetWarehouseById(warehouseId);
            return warehouse != null ? warehouse.GetLowStockItems(threshold) : Result<List<Item>>.Failure("Warehouse not found.");
        }
        /// <summary>
        /// Calculate the total value of all items in a warehouse
        /// </summary>
        /// <param name="warehouseId"></param>
        /// <returns></returns>
        public Result<decimal> CalculateTotalInventoryValue(int warehouseId)
        {
            var warehouse = GetWarehouseById(warehouseId);
            return warehouse != null ? warehouse.GetTotalInventoryValue() : Result<decimal>.Failure("Warehouse not found.");
        }
        /// <summary>
        /// Generate an inventory report for a warehouse
        /// </summary>
        /// <param name="warehouseId"></param>
        /// <returns></returns>
        public Result<string> GenerateInventoryReport(int warehouseId)
        {
            var warehouse = GetWarehouseById(warehouseId);
            return warehouse != null ? warehouse.GenerateInventoryReport() : Result<string>.Failure("Warehouse not found.");
        }
    }
}
