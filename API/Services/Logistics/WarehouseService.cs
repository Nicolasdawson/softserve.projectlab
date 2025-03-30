using System.Collections.Generic;
using System.Linq;
using API.Data;
using API.Data.Entities;
using API.Models;
using API.Models.IntAdmin;
using API.Models.Logistics;
using API.Models.Logistics.Interfaces;
using API.Services.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class WarehouseService : IWarehouseService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public WarehouseService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<API.Models.Logistics.Warehouse>> GetWarehousesAsync()
        {
            var warehouses = await _context.Warehouses
                                           .Include(w => w.Branch)  
                                           .ToListAsync();

            // Map the entities to the model using AutoMapper (ensure we map from entity to model)
            return _mapper.Map<List<API.Models.Logistics.Warehouse>>(warehouses);  // Map to the model class, not the interface
        }


        /// <summary>
        /// Get a warehouse by its ID
        /// </summary>
        /// <param name="warehouseId"></param>
        /// <returns></returns>
        private Warehouse GetWarehouseById(int warehouseId)
        {
            var warehouseEntity = _context.Warehouses.FirstOrDefault(w => w.WarehouseId == warehouseId);
            return warehouseEntity != null ? _mapper.Map<Warehouse>(warehouseEntity) : null;
        }

        /// <summary>
        /// Add an item to a warehouse  
        /// </summary>
        /// <param name="warehouseId"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task<Result<IWarehouse>> AddItemToWarehouseAsync(int warehouseId, Item item)
        {
            // Fetch the warehouse entity from the database
            var warehouseEntity = await _context.Warehouses
                                                .FirstOrDefaultAsync(w => w.WarehouseId == warehouseId);

            if (warehouseEntity == null)
            {
                return Result<IWarehouse>.Failure("Warehouse not found.");
            }

            // Ensure the item exists in the Item table, if not, add it
            var itemEntity = await _context.Items
                                            .FirstOrDefaultAsync(i => i.Sku == item.Sku);
            if (itemEntity == null)
            {
                itemEntity = new ItemEntity
                {
                    Sku = item.Sku,
                    // Map other properties of item
                };

                _context.Items.Add(itemEntity);
                await _context.SaveChangesAsync(); // Save the new item to the Item table
            }

            // Add the item to the warehouse by inserting into WarehouseItemEntity table
            var warehouseItem = new WarehouseItemEntity
            {
                WarehouseId = warehouseId,
                Sku = item.Sku, // Link the item using SKU
                Stock = item.CurrentStock // Set the stock of the item in the warehouse
            };

            _context.WarehouseItems.Add(warehouseItem);
            await _context.SaveChangesAsync(); // Commit the changes

            // Map WarehouseEntity to the concrete Warehouse class
            var warehouse = _mapper.Map<Warehouse>(warehouseEntity);

            // Return success with the warehouse
            return Result<IWarehouse>.Success(warehouse);
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