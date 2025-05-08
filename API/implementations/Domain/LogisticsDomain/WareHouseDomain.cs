using API.Models.IntAdmin;
using API.Models.Logistics.Interfaces;
using API.Data.Entities;
using API.Models;
using API.Data;
using API.Models.Logistics;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using softserve.projectlabs.Shared.Utilities;
using softserve.projectlabs.Shared.DTOs;

namespace API.Implementations.Domain
{
    public class WarehouseDomain
    {
        private readonly ApplicationDbContext _context;

        public WarehouseDomain(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<bool>> AddItemToWarehouseAsync(AddItemToWarehouseDto addItemDto)
        {
            try
            {
                // Check if the item exists
                var existingItem = await _context.ItemEntities
                    .FirstOrDefaultAsync(i => i.Sku == addItemDto.Sku);

                if (existingItem == null)
                {
                    return Result<bool>.Failure($"Item with SKU {addItemDto.Sku} does not exist in the system. Please create the item first.");
                }

                // Check if the warehouse exists
                var warehouse = await _context.WarehouseEntities
                    .FirstOrDefaultAsync(w => w.WarehouseId == addItemDto.WarehouseId);

                if (warehouse == null)
                {
                    return Result<bool>.Failure($"Warehouse with ID {addItemDto.WarehouseId} does not exist.");
                }

                // Check if the item is already linked to the warehouse
                var warehouseItem = await _context.WarehouseItemEntities
                    .FirstOrDefaultAsync(wi => wi.WarehouseId == addItemDto.WarehouseId && wi.Sku == addItemDto.Sku);

                if (warehouseItem != null)
                {
                    // Update the quantity explicitly
                    warehouseItem.ItemQuantity = addItemDto.CurrentStock;
                    _context.WarehouseItemEntities.Update(warehouseItem);
                }
                else
                {
                    // Add a new warehouse item
                    var newWarehouseItem = new WarehouseItemEntity
                    {
                        WarehouseId = addItemDto.WarehouseId,
                        Sku = existingItem.Sku,
                        ItemQuantity = addItemDto.CurrentStock // Set explicitly
                    };

                    _context.WarehouseItemEntities.Add(newWarehouseItem);
                }

                await _context.SaveChangesAsync();

                return Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure($"Failed to add item to warehouse: {ex.Message}");
            }
        }

        public async Task<Result<bool>> SoftDeleteWarehouseAsync(int warehouseId)
        {
            try
            {
                var warehouse = await _context.WarehouseEntities
                    .FirstOrDefaultAsync(w => w.WarehouseId == warehouseId);

                if (warehouse == null)
                {
                    return Result<bool>.Failure("Warehouse not found.");
                }

                warehouse.IsDeleted = true; // Mark as deleted
                _context.WarehouseEntities.Update(warehouse);
                await _context.SaveChangesAsync();

                return Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure($"Failed to delete warehouse: {ex.Message}");
            }
        }

        public async Task<Result<bool>> UndeleteWarehouseAsync(int warehouseId)
        {
            try
            {
                var warehouse = await _context.WarehouseEntities
                    .FirstOrDefaultAsync(w => w.WarehouseId == warehouseId);

                if (warehouse == null)
                {
                    return Result<bool>.Failure("Warehouse not found.");
                }

                if (!warehouse.IsDeleted)
                {
                    return Result<bool>.Failure("Warehouse is already active.");
                }

                warehouse.IsDeleted = false; // Restore the warehouse
                _context.WarehouseEntities.Update(warehouse);
                await _context.SaveChangesAsync();

                return Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure($"Failed to restore warehouse: {ex.Message}");
            }
        }

        public async Task<WarehouseEntity?> GetWarehouseByNameAsync(string name)
        {
            return await _context.WarehouseEntities
                .FirstOrDefaultAsync(w => w.WarehouseLocation == name && !w.IsDeleted);
        }

        public async Task<Result<Warehouse>> CreateWarehouseAsync(WarehouseDto warehouseDto)
        {
            try
            {
                var warehouseEntity = new WarehouseEntity
                {
                    WarehouseLocation = warehouseDto.Location,
                    WarehouseCapacity = warehouseDto.Capacity,
                    BranchId = warehouseDto.BranchId,
                    IsDeleted = false
                };

                _context.WarehouseEntities.Add(warehouseEntity);
                await _context.SaveChangesAsync();

                var newWarehouseDto = new WarehouseDto
                {
                    WarehouseId = warehouseEntity.WarehouseId,
                    Name = $"Warehouse {warehouseEntity.WarehouseId}",
                    Location = warehouseEntity.WarehouseLocation,
                    Capacity = warehouseEntity.WarehouseCapacity,
                    BranchId = warehouseEntity.BranchId
                };

                var warehouse = new Warehouse(newWarehouseDto);

                return Result<Warehouse>.Success(warehouse);
            }
            catch (Exception ex)
            {
                return Result<Warehouse>.Failure($"Failed to create warehouse: {ex.Message}");
            }
        }

        public async Task<Result<Warehouse>> GetWarehouseByIdAsync(int warehouseId)
        {
            try
            {
                var entity = await _context.WarehouseEntities
                    .Where(w => !w.IsDeleted)
                    .Include(w => w.WarehouseItemEntities)
                    .ThenInclude(wi => wi.SkuNavigation)
                    .FirstOrDefaultAsync(w => w.WarehouseId == warehouseId);

                if (entity == null)
                    return Result<Warehouse>.Failure("Warehouse not found", 404);

                // Map WarehouseEntity to WarehouseDto
                var warehouseDto = new WarehouseDto
                {
                    WarehouseId = entity.WarehouseId,
                    Name = $"Warehouse {entity.WarehouseId}",
                    Location = entity.WarehouseLocation,
                    Capacity = entity.WarehouseCapacity,
                    BranchId = entity.BranchId
                };

                // Create a new Warehouse instance
                var warehouse = new Warehouse(warehouseDto);

                // Populate items
                warehouse.Items = entity.WarehouseItemEntities.Select(wi => new Item
                {
                    ItemId = wi.Sku,
                    Sku = wi.Sku,
                    ItemName = wi.SkuNavigation.ItemName,
                    ItemDescription = wi.SkuNavigation.ItemDescription,
                    CurrentStock = wi.ItemQuantity
                }).ToList();

                return Result<Warehouse>.Success(warehouse);
            }
            catch (Exception ex)
            {
                return Result<Warehouse>.Failure($"Failed to retrieve warehouse: {ex.Message}", 500, ex.StackTrace);
            }
        }

        public async Task<Result<List<Warehouse>>> GetAllWarehousesAsync()
        {
            try
            {
                var entities = await _context.WarehouseEntities
                    .Where(w => !w.IsDeleted)
                    .Include(w => w.Branch)
                    .Include(w => w.WarehouseItemEntities)
                    .ThenInclude(wi => wi.SkuNavigation)
                    .ToListAsync();

                // Map List<WarehouseEntity> to List<Warehouse>
                var warehouses = entities.Select(entity =>
                {
                    var warehouseDto = new WarehouseDto
                    {
                        WarehouseId = entity.WarehouseId,
                        Name = $"Warehouse {entity.WarehouseId}",
                        Location = entity.WarehouseLocation,
                        Capacity = entity.WarehouseCapacity,
                        BranchId = entity.BranchId
                    };

                    var warehouse = new Warehouse(warehouseDto);

                    // Populate items
                    warehouse.Items = entity.WarehouseItemEntities.Select(wi => new Item
                    {
                        ItemId = wi.Sku,
                        ItemName = wi.SkuNavigation.ItemName,
                        ItemDescription = wi.SkuNavigation.ItemDescription,
                        CurrentStock = wi.ItemQuantity
                    }).ToList();

                    return warehouse;
                }).ToList();

                return Result<List<Warehouse>>.Success(warehouses);
            }
            catch (Exception ex)
            {
                return Result<List<Warehouse>>.Failure($"Failed to retrieve warehouses: {ex.Message}", 500, ex.StackTrace);
            }
        }

        public async Task<Result<Warehouse>> RemoveItemFromWarehouseAsync(int warehouseId, int itemId)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var warehouseEntity = await _context.WarehouseEntities
                    .Include(w => w.WarehouseItemEntities)
                    .FirstOrDefaultAsync(w => w.WarehouseId == warehouseId);

                if (warehouseEntity == null)
                    return Result<Warehouse>.Failure("Warehouse not found", 404);

                var warehouseItem = warehouseEntity.WarehouseItemEntities
                    .FirstOrDefault(wi => wi.Sku == itemId);

                if (warehouseItem == null)
                    return Result<Warehouse>.Failure("Item not found in warehouse", 404);

                _context.WarehouseItemEntities.Remove(warehouseItem);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                // Map WarehouseEntity to WarehouseDto
                var warehouseDto = new WarehouseDto
                {
                    WarehouseId = warehouseEntity.WarehouseId,
                    Name = $"Warehouse {warehouseEntity.WarehouseId}",
                    Location = warehouseEntity.WarehouseLocation,
                    Capacity = warehouseEntity.WarehouseCapacity,
                    BranchId = warehouseEntity.BranchId
                };

                var warehouse = new Warehouse(warehouseDto);

                // Populate items excluding the removed one
                warehouse.Items = warehouseEntity.WarehouseItemEntities
                    .Where(wi => wi.Sku != itemId)
                    .Select(wi => new Item
                    {
                        ItemId = wi.Sku,
                        ItemName = wi.SkuNavigation.ItemName,
                        ItemDescription = wi.SkuNavigation.ItemDescription,
                        CurrentStock = wi.ItemQuantity
                    }).ToList();

                return Result<Warehouse>.Success(warehouse);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return Result<Warehouse>.Failure($"Failed to remove item: {ex.Message}", 500, ex.StackTrace);
            }
        }

        public async Task<Result<bool>> TransferItemAsync(
         Warehouse source,
         int sku,
         int quantity,
         Warehouse target)
        {
            var executionStrategy = _context.Database.CreateExecutionStrategy();

            return await executionStrategy.ExecuteAsync(async () =>
            {
                using var transaction = await _context.Database.BeginTransactionAsync();
                try
                {
                    var sourceData = source.GetWarehouseData();
                    var targetData = target.GetWarehouseData();

                    var sourceItem = await _context.WarehouseItemEntities
                        .FirstOrDefaultAsync(wi => wi.WarehouseId == sourceData.WarehouseId && wi.Sku == sku);

                    if (sourceItem == null || sourceItem.ItemQuantity < quantity)
                        return Result<bool>.Failure("Insufficient stock for transfer", 400);

                    // Deduct quantity from source warehouse
                    sourceItem.ItemQuantity -= quantity;
                    _context.WarehouseItemEntities.Update(sourceItem);

                    // Add or update quantity in target warehouse
                    var targetItem = await _context.WarehouseItemEntities
                        .FirstOrDefaultAsync(wi => wi.WarehouseId == targetData.WarehouseId && wi.Sku == sku);

                    if (targetItem == null)
                    {
                        await _context.WarehouseItemEntities.AddAsync(new WarehouseItemEntity
                        {
                            WarehouseId = targetData.WarehouseId,
                            Sku = sku,
                            ItemQuantity = quantity
                        });
                    }
                    else
                    {
                        targetItem.ItemQuantity += quantity;
                        _context.WarehouseItemEntities.Update(targetItem);
                    }

                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();

                    return Result<bool>.Success(true);
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    return Result<bool>.Failure($"Transfer failed: {ex.Message}", 500);
                }
            });
        }
    }
}
