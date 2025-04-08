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
        Task<List<Warehouse>> GetWarehousesAsync();
        Task<Result<Warehouse>> GetWarehouseByIdAsync(int warehouseId);
        Task<Result<IWarehouse>> AddItemToWarehouseAsync(int warehouseId, Item item);
        Task<Result<bool>> RemoveItemFromWarehouseAsync(int warehouseId, int itemId);
        Task<Result<int>> CheckWarehouseStockAsync(int warehouseId, int sku);
        Task<Result<bool>> TransferItemAsync(int sourceWarehouseId, int sku, int quantity, int targetWarehouseId);
        Task<Result<List<Item>>> GetLowStockItemsAsync(int warehouseId, int threshold);
        Task<Result<decimal>> CalculateTotalInventoryValueAsync(int warehouseId);
        Task<Result<string>> GenerateInventoryReportAsync(int warehouseId);
    }
}