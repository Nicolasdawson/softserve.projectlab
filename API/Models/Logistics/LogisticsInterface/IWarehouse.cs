using API.Data.Entities;
using API.Models.IntAdmin;
using System.Collections.Generic;

namespace API.Models.Logistics.Interfaces
{
    public interface IWarehouse
    {
        int WarehouseId { get; set; }
        string Name { get; set; }
        string Location { get; set; }
        int Capacity { get; set; }
        List<Item> Items { get; set; }

        // Stock Management
        Result<IWarehouse> AddItem(Item item);
        Result<IWarehouse> RemoveItem(Item item);
        Result<IWarehouse> GetAvailableStock(int sku);
        Result<bool> UpdateItemStock(int sku, int quantity);
        Result<int> CheckItemStock(int sku);
        Result<bool> IsItemInStock(int sku, int requiredQuantity);

        // Warehouse Operations
        Result<bool> TransferItem(int sku, int quantity, IWarehouse targetWarehouse);
        Result<List<Item>> GetLowStockItems(int threshold);
        Result<List<Item>> GetOutOfStockItems();

        // Order Processing
        Result<bool> ReserveStockForOrder(int sku, int quantity);
        Result<bool> ReleaseReservedStock(int sku, int quantity);
        Result<bool> ShipItems(List<Item> items);

        // Reporting
        Result<decimal> GetTotalInventoryValue();
        Result<string> GenerateInventoryReport();
    }
}
