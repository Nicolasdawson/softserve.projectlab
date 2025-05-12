using API.Models.IntAdmin;
using softserve.projectlabs.Shared.DTOs;
using System.Collections.Generic;

namespace API.Models.Logistics.Interfaces
{
    public interface IWarehouse
    {
        List<Item> Items { get; set; }

        // Core State Management
        void AddItem(Item item);
        void RemoveItem(int sku);
        void UpdateItemStock(int sku, int quantity);
        bool IsItemInStock(int sku, int requiredQuantity);
        void TransferItem(int sku, int quantity, IWarehouse targetWarehouse);

        // Metadata
        WarehouseDto GetWarehouseData();
    }
}
