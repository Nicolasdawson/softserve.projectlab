using API.Models.Logistics;
using API.Data.Entities;
using API.Data.Repositories.LogisticsRepositories.Interfaces;
using softserve.projectlabs.Shared.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models.IntAdmin;
using softserve.projectlabs.Shared.DTOs;

namespace API.Implementations.Domain
{
    public class WarehouseDomain
    {
        private readonly IWarehouseRepository _warehouseRepository;

        public WarehouseDomain(IWarehouseRepository warehouseRepository)
        {
            _warehouseRepository = warehouseRepository;
        }

        // Mapping: Domain Model → Data Model
        private WarehouseEntity MapToEntity(Warehouse warehouse)
        {
            return new WarehouseEntity
            {
                WarehouseId = warehouse.GetWarehouseData().WarehouseId,
                WarehouseLocation = warehouse.GetWarehouseData().Location,
                WarehouseCapacity = warehouse.GetWarehouseData().Capacity,
                BranchId = warehouse.GetWarehouseData().BranchId,
                IsDeleted = false,
                WarehouseItemEntities = warehouse.Items.Select(item => new WarehouseItemEntity
                {
                    Sku = item.Sku,
                    ItemQuantity = item.CurrentStock
                }).ToList()
            };
        }

        // Mapping: Data Model → Domain Model
        private Warehouse MapToDomainModel(WarehouseEntity entity)
        {
            var warehouseDto = new WarehouseDto
            {
                WarehouseId = entity.WarehouseId,
                Name = $"Warehouse {entity.WarehouseId}",
                Location = entity.WarehouseLocation,
                Capacity = entity.WarehouseCapacity,
                BranchId = entity.BranchId
            };

            var warehouse = new Warehouse(warehouseDto)
            {
                Items = entity.WarehouseItemEntities.Select(wi => new Item
                {
                    ItemId = wi.Sku,
                    Sku = wi.Sku,
                    ItemName = wi.SkuNavigation.ItemName,
                    ItemDescription = wi.SkuNavigation.ItemDescription,
                    CurrentStock = wi.ItemQuantity
                }).ToList()
            };

            return warehouse;
        }

        // Reserve Stock for an Order
        public async Task<Result<bool>> ReserveStockForOrderAsync(int warehouseId, int sku, int quantity)
        {
            try
            {
                var warehouseEntity = await _warehouseRepository.GetByIdAsync(warehouseId);
                if (warehouseEntity == null)
                    return Result<bool>.Failure("Warehouse not found.");

                var warehouse = MapToDomainModel(warehouseEntity);
                var item = warehouse.Items.FirstOrDefault(i => i.Sku == sku);

                if (item == null || item.CurrentStock < quantity)
                    return Result<bool>.Failure("Insufficient stock for reservation.");

                // Reserve the stock
                item.CurrentStock -= quantity;

                // Update the warehouse in the database
                await _warehouseRepository.UpdateAsync(MapToEntity(warehouse));
                return Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure($"Failed to reserve stock: {ex.Message}");
            }
        }

        // Get Low Stock Items
        public async Task<Result<List<Item>>> GetLowStockItemsAsync(int warehouseId, int threshold)
        {
            var warehouseResult = await GetWarehouseByIdAsync(warehouseId);
            if (!warehouseResult.IsSuccess)
                return Result<List<Item>>.Failure(warehouseResult.ErrorMessage);

            var lowStockItems = warehouseResult.Data.Items.Where(item => item.CurrentStock <= threshold).ToList();
            return Result<List<Item>>.Success(lowStockItems);
        }

        // Get Out of Stock Items
        public async Task<Result<List<Item>>> GetOutOfStockItemsAsync(int warehouseId)
        {
            var warehouseResult = await GetWarehouseByIdAsync(warehouseId);
            if (!warehouseResult.IsSuccess)
                return Result<List<Item>>.Failure(warehouseResult.ErrorMessage);

            var outOfStockItems = warehouseResult.Data.Items.Where(item => item.CurrentStock == 0).ToList();
            return Result<List<Item>>.Success(outOfStockItems);
        }

        // Generate Inventory Report
        public async Task<Result<string>> GenerateInventoryReportAsync(int warehouseId)
        {
            var warehouseResult = await GetWarehouseByIdAsync(warehouseId);
            if (!warehouseResult.IsSuccess)
                return Result<string>.Failure(warehouseResult.ErrorMessage);

            var report = System.Text.Json.JsonSerializer.Serialize(warehouseResult.Data.Items);
            return Result<string>.Success(report);
        }

        // Get Total Inventory Value
        public async Task<Result<decimal>> GetTotalInventoryValueAsync(int warehouseId)
        {
            var warehouseResult = await GetWarehouseByIdAsync(warehouseId);
            if (!warehouseResult.IsSuccess)
                return Result<decimal>.Failure(warehouseResult.ErrorMessage);

            var totalValue = warehouseResult.Data.Items.Sum(item => item.CurrentStock * item.ItemPrice);
            return Result<decimal>.Success(totalValue);
        }

        // Get Warehouse by ID
        public async Task<Result<Warehouse>> GetWarehouseByIdAsync(int warehouseId)
        {
            try
            {
                var entity = await _warehouseRepository.GetByIdAsync(warehouseId);
                if (entity == null)
                    return Result<Warehouse>.Failure("Warehouse not found.");

                var warehouse = MapToDomainModel(entity);
                return Result<Warehouse>.Success(warehouse);
            }
            catch (Exception ex)
            {
                return Result<Warehouse>.Failure($"Failed to retrieve warehouse: {ex.Message}");
            }
        }

        // Soft Delete Warehouse
        public async Task<Result<bool>> SoftDeleteWarehouseAsync(int warehouseId)
        {
            try
            {
                var warehouseEntity = await _warehouseRepository.GetByIdAsync(warehouseId);
                if (warehouseEntity == null)
                    return Result<bool>.Failure("Warehouse not found.");

                warehouseEntity.IsDeleted = true;
                await _warehouseRepository.UpdateAsync(warehouseEntity);
                return Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure($"Failed to delete warehouse: {ex.Message}");
            }
        }

        // Undelete Warehouse
        public async Task<Result<bool>> UndeleteWarehouseAsync(int warehouseId)
        {
            try
            {
                var warehouseEntity = await _warehouseRepository.GetByIdAsync(warehouseId);
                if (warehouseEntity == null)
                    return Result<bool>.Failure("Warehouse not found.");

                warehouseEntity.IsDeleted = false;
                await _warehouseRepository.UpdateAsync(warehouseEntity);
                return Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure($"Failed to restore warehouse: {ex.Message}");
            }
        }

        // Update Warehouse
        public async Task<Result<bool>> UpdateWarehouseAsync(Warehouse warehouse)
        {
            try
            {
                var warehouseEntity = MapToEntity(warehouse);
                await _warehouseRepository.UpdateAsync(warehouseEntity);
                return Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure($"Failed to update warehouse: {ex.Message}");
            }
        }

        // Get All Warehouses
        public async Task<Result<List<Warehouse>>> GetAllWarehousesAsync()
        {
            try
            {
                var entities = await _warehouseRepository.GetAllAsync();
                var warehouses = entities.Select(MapToDomainModel).ToList();
                return Result<List<Warehouse>>.Success(warehouses);
            }
            catch (Exception ex)
            {
                return Result<List<Warehouse>>.Failure($"Failed to retrieve warehouses: {ex.Message}");
            }
        }

        // Create Warehouse
        public async Task<Result<Warehouse>> CreateWarehouseAsync(WarehouseDto warehouseDto)
        {
            try
            {
                // Map WarehouseDto to WarehouseEntity
                var warehouseEntity = new WarehouseEntity
                {
                    WarehouseLocation = warehouseDto.Location,
                    WarehouseCapacity = warehouseDto.Capacity,
                    BranchId = warehouseDto.BranchId,
                    IsDeleted = false
                };

                // Save the WarehouseEntity to the database
                await _warehouseRepository.AddAsync(warehouseEntity);

                // Map the saved entity back to a domain model
                var warehouse = MapToDomainModel(warehouseEntity);

                return Result<Warehouse>.Success(warehouse);
            }
            catch (Exception ex)
            {
                return Result<Warehouse>.Failure($"Failed to create warehouse: {ex.Message}");
            }
        }

    }
}

