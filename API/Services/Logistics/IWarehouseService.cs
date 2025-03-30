using System.Collections.Generic;
using API.Data.Entities;
using API.Models.IntAdmin;
using API.Models.Logistics;
using API.Models.Logistics.Interfaces;
using API.Models;


namespace API.Services.Interfaces
{
    public interface IWarehouseService
    {
        Task<Result<IWarehouse>> AddItemToWarehouseAsync(int warehouseId, Item item);
        Result<IWarehouse> RemoveItemFromWarehouse(int warehouseId, Item item);
        Result<int> CheckWarehouseStock(int warehouseId, int sku);
        Result<bool> TransferItem(int warehouseId, int sku, int quantity, int targetWarehouseId);
        Result<List<Item>> GetLowStockItems(int warehouseId, int threshold);
        Result<decimal> CalculateTotalInventoryValue(int warehouseId);
        Result<string> GenerateInventoryReport(int warehouseId);

        Task<List<Warehouse>> GetWarehousesAsync();

    }
}