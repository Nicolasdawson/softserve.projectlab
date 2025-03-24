using System.Collections.Generic;
using API.Models;
using API.Models.IntAdmin;
using API.Models.Logistics.Interfaces;

namespace API.Services.Interfaces
{
    public interface IWarehouseService
    {
        Result<IWarehouse> AddItemToWarehouse(int warehouseId, Item item);
        Result<IWarehouse> RemoveItemFromWarehouse(int warehouseId, Item item);
        Result<int> CheckWarehouseStock(int warehouseId, int sku);
        Result<bool> TransferItem(int warehouseId, int sku, int quantity, int targetWarehouseId);
        Result<List<Item>> GetLowStockItems(int warehouseId, int threshold);
        Result<decimal> CalculateTotalInventoryValue(int warehouseId);
        Result<string> GenerateInventoryReport(int warehouseId);
    }
}
