using API.Models.IntAdmin;
using softserve.projectlabs.Shared.Utilities;

namespace API.Models.Logistics.Interfaces
{
    public interface IWarehouse
    {
        List<Item> Items { get; set; }

        // Stock Management
        Task<Result<IWarehouse>> AddItemAsync(Item item);
        Task<Result<IWarehouse>> RemoveItemAsync(Item item);
        Task<Result<IWarehouse>> GetAvailableStockAsync(int sku);
        Task<Result<bool>> UpdateItemStockAsync(int sku, int quantity);
        Task<Result<int>> CheckItemStockAsync(int sku);
        Task<Result<bool>> IsItemInStockAsync(int sku, int requiredQuantity);

        // Warehouse Operations
        Task<Result<bool>> TransferItemAsync(int sku, int quantity, IWarehouse targetWarehouse);
        Task<Result<List<Item>>> GetLowStockItemsAsync(int threshold);
        Task<Result<List<Item>>> GetOutOfStockItemsAsync();

        // Order Processing
        Task<Result<bool>> ReserveStockForOrderAsync(int sku, int quantity);
        Task<Result<bool>> ReleaseReservedStockAsync(int sku, int quantity);
        Task<Result<bool>> ShipItemsAsync(List<Item> items);

        // Reporting
        Task<Result<decimal>> GetTotalInventoryValueAsync();
        Task<Result<string>> GenerateInventoryReportAsync();

        // Optional: Keep sync versions if needed for backwards compatibility
        Result<decimal> GetTotalInventoryValue();
        Result<string> GenerateInventoryReport();
    }
}
