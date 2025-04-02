using System;
using System.Collections.Generic;
using System.Linq;
using API.Models;
using API.Models.IntAdmin;
using API.Models.Logistics.Interfaces;

namespace API.Models.Logistics
{
    public class Warehouse : IWarehouse
    {
        public int WarehouseId { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public int Capacity { get; set; }
        public List<Item> Items { get; set; } = new List<Item>();
        public int BranchId { get; internal set; }
        public Data.Entities.BranchEntity Branch { get; internal set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="warehouseId"></param>
        /// <param name="name"></param>
        /// <param name="location"></param>
        /// <param name="capacity"></param>        

        // Parameterless constructor for Dependency Injection (DI) & serialization
        public Warehouse() { }

        public Warehouse(int warehouseId, string name, string location, int capacity)
        {
            WarehouseId = warehouseId;
            Name = name;
            Location = location;
            Capacity = capacity;
        }

        /// <summary>
        /// Add an item to the warehouse
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public Result<IWarehouse> AddItem(Item item)
        {
            Items.Add(item);
            return Result<IWarehouse>.Success(this);
        }

        /// <summary>
        /// Remove an item from the warehouse
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public Result<IWarehouse> RemoveItem(Item item)
        {
            var existingItem = Items.FirstOrDefault(i => i.Sku == item.Sku);
            if (existingItem != null)
            {
                Items.Remove(existingItem);
                return Result<IWarehouse>.Success(this);
            }
            return Result<IWarehouse>.Failure("Item not found in warehouse.");
        }

        /// <summary>
        /// Get available stock for a SKU
        /// </summary>
        /// <param name="sku"></param>
        /// <returns></returns>
        public Result<IWarehouse> GetAvailableStock(int sku)
        {
            var stock = Items.FirstOrDefault(i => i.Sku == sku);
            return stock != null
                ? Result<IWarehouse>.Success(this)
                : Result<IWarehouse>.Failure("Item not found.");
        }

        /// <summary>
        /// Update item stock
        /// </summary>
        /// <param name="sku"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        public Result<bool> UpdateItemStock(int sku, int quantity)
        {
            var item = Items.FirstOrDefault(i => i.Sku == sku);
            if (item != null)
            {
                item.CurrentStock += quantity;
                return Result<bool>.Success(true);
            }
            return Result<bool>.Failure("Item not found.");
        }

        /// <summary>
        /// Check stock level
        /// </summary>
        /// <param name="sku"></param>
        /// <returns></returns>
        public Result<int> CheckItemStock(int sku)
        {
            var item = Items.FirstOrDefault(i => i.Sku == sku);
            return item != null ? Result<int>.Success(item.CurrentStock) : Result<int>.Failure("Item not found.");
        }

        /// <summary>
        /// Verify if enough stock exists
        /// </summary>
        /// <param name="sku"></param>
        /// <param name="requiredQuantity"></param>
        /// <returns></returns>
        public Result<bool> IsItemInStock(int sku, int requiredQuantity)
        {
            var item = Items.FirstOrDefault(i => i.Sku == sku);
            return item != null && item.CurrentStock >= requiredQuantity
                ? Result<bool>.Success(true)
                : Result<bool>.Failure("Insufficient stock.");
        }

        /// <summary>
        /// Transfer items to another warehouse
        /// </summary>
        /// <param name="sku"></param>
        /// <param name="quantity"></param>
        /// <param name="targetWarehouse"></param>
        /// <returns></returns>
        public Result<bool> TransferItem(int sku, int quantity, IWarehouse targetWarehouse)
        {
            var item = Items.FirstOrDefault(i => i.Sku == sku);
            if (item != null && item.CurrentStock >= quantity)
            {
                item.CurrentStock -= quantity;
                targetWarehouse.AddItem(new Item
                {
                    Sku = item.Sku,
                    //ItemName = item.ItemName,
                    CurrentStock = quantity
                });
                return Result<bool>.Success(true);
            }
            return Result<bool>.Failure("Not enough stock or item not found.");
        }

        /// <summary>
        /// Get items below a certain stock level
        /// </summary>
        /// <param name="threshold"></param>
        /// <returns></returns>
        public Result<List<Item>> GetLowStockItems(int threshold)
        {
            var lowStockItems = Items.Where(i => i.CurrentStock <= threshold).ToList();
            return Result<List<Item>>.Success(lowStockItems);
        }

        /// <summary>
        /// Get out-of-stock items
        /// </summary>
        /// <returns></returns>
        public Result<List<Item>> GetOutOfStockItems()
        {
            var outOfStockItems = Items.Where(i => i.CurrentStock == 0).ToList();
            return Result<List<Item>>.Success(outOfStockItems);
        }

        /// <summary>
        /// Reserve stock for an order
        /// </summary>
        /// <param name="sku"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        public Result<bool> ReserveStockForOrder(int sku, int quantity)
        {
            var item = Items.FirstOrDefault(i => i.Sku == sku);
            if (item != null && item.CurrentStock >= quantity)
            {
                item.CurrentStock -= quantity;
                return Result<bool>.Success(true);
            }
            return Result<bool>.Failure("Insufficient stock for reservation.");
        }

        /// <summary>
        /// Release reserved stock (if an order is canceled)
        /// </summary>
        /// <param name="sku"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        public Result<bool> ReleaseReservedStock(int sku, int quantity)
        {
            var item = Items.FirstOrDefault(i => i.Sku == sku);
            if (item != null)
            {
                item.CurrentStock += quantity;
                return Result<bool>.Success(true);
            }
            return Result<bool>.Failure("Item not found.");
        }

        /// <summary>
        /// Process shipments
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public Result<bool> ShipItems(List<Item> items)
        {
            foreach (var item in items)
            {
                var warehouseItem = Items.FirstOrDefault(i => i.Sku == item.Sku);
                if (warehouseItem == null || warehouseItem.CurrentStock < item.CurrentStock)
                    return Result<bool>.Failure($"Not enough stock for item: {item.Name}");

                warehouseItem.CurrentStock -= item.CurrentStock;
            }
            return Result<bool>.Success(true);
        }

        /// <summary>
        /// Calculate total inventory value
        /// </summary>
        /// <returns></returns>
        public Result<decimal> GetTotalInventoryValue()
        {
            var totalValue = Items.Sum(i => i.CurrentStock * i.ItemPrice);
            return Result<decimal>.Success(totalValue);
        }

        /// <summary>
        /// Generate inventory report (JSON format)
        /// </summary>
        /// <returns></returns>
        public Result<string> GenerateInventoryReport()
        {
            var report = System.Text.Json.JsonSerializer.Serialize(Items);
            return Result<string>.Success(report);
        }
    }
}