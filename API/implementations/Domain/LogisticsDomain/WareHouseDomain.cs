using API.Models.IntAdmin;
using API.Models.Logistics.Interfaces;
using API.Data.Entities;
using API.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using softserve.projectlabs.Shared.Utilities;
using softserve.projectlabs.Shared.DTOs;
using API.Models.Logistics;
using AutoMapper;
using API.Data.Repositories.LogisticsRepositories.Interfaces;

namespace API.Implementations.Domain
{
    public class WarehouseDomain
    {
        private readonly IWarehouseRepository _warehouseRepository;
        private readonly IMapper _mapper;

        public WarehouseDomain(IWarehouseRepository warehouseRepository, IMapper mapper)
        {
            _warehouseRepository = warehouseRepository;
            _mapper = mapper;
        }

        public async Task<Result<bool>> AddItemToWarehouseAsync(AddItemToWarehouseDto addItemDto)
        {
            try
            {
                var warehouse = await _warehouseRepository.GetByIdAsync(addItemDto.WarehouseId);
                if (warehouse == null)
                {
                    return Result<bool>.Failure($"Warehouse with ID {addItemDto.WarehouseId} does not exist.");
                }

                var warehouseItem = warehouse.WarehouseItemEntities
                    .FirstOrDefault(wi => wi.Sku == addItemDto.Sku);

                if (warehouseItem != null)
                {
                    warehouseItem.ItemQuantity = addItemDto.CurrentStock;
                }
                else
                {
                    warehouse.WarehouseItemEntities.Add(new WarehouseItemEntity
                    {
                        WarehouseId = addItemDto.WarehouseId,
                        Sku = addItemDto.Sku,
                        ItemQuantity = addItemDto.CurrentStock
                    });
                }

                await _warehouseRepository.UpdateAsync(warehouse);
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
                await _warehouseRepository.DeleteAsync(warehouseId);
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
                var warehouse = await _warehouseRepository.GetByIdAsync(warehouseId);
                if (warehouse == null)
                {
                    return Result<bool>.Failure("Warehouse not found.");
                }

                if (!warehouse.IsDeleted)
                {
                    return Result<bool>.Failure("Warehouse is already active.");
                }

                warehouse.IsDeleted = false;
                await _warehouseRepository.UpdateAsync(warehouse);
                return Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure($"Failed to restore warehouse: {ex.Message}");
            }
        }

        public async Task<WarehouseEntity?> GetWarehouseByNameAsync(string name)
        {
            return await _warehouseRepository.GetByNameAsync(name);
        }

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

                await _warehouseRepository.AddAsync(warehouseEntity);

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
                var entity = await _warehouseRepository.GetByIdAsync(warehouseId);
                if (entity == null)
                    return Result<Warehouse>.Failure("Warehouse not found", 404);

                var warehouseDto = new WarehouseDto
                {
                    WarehouseId = entity.WarehouseId,
                    Name = $"Warehouse {entity.WarehouseId}",
                    Location = entity.WarehouseLocation,
                    Capacity = entity.WarehouseCapacity,
                    BranchId = entity.BranchId
                };

                var warehouse = new Warehouse(warehouseDto);
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
                var entities = await _warehouseRepository.GetAllAsync();
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

        public async Task<Result<bool>> RemoveItemFromWarehouseAsync(int warehouseId, int itemId)
        {
            var warehouse = await _warehouseRepository.GetByIdAsync(warehouseId);
            if (warehouse == null)
                return Result<bool>.Failure("Warehouse not found.");

            var warehouseItem = warehouse.WarehouseItemEntities.FirstOrDefault(wi => wi.Sku == itemId);
            if (warehouseItem == null)
                return Result<bool>.Failure("Item not found in warehouse.");

            warehouse.WarehouseItemEntities.Remove(warehouseItem);
            await _warehouseRepository.UpdateAsync(warehouse);

            return Result<bool>.Success(true);
        }

        public async Task<Result<bool>> TransferItemAsync(Warehouse source, int sku, int quantity, Warehouse target)
        {
            var sourceItem = source.Items.FirstOrDefault(item => item.Sku == sku);
            if (sourceItem == null || sourceItem.CurrentStock < quantity)
                return Result<bool>.Failure("Insufficient stock in source warehouse.");

            sourceItem.CurrentStock -= quantity;

            var targetItem = target.Items.FirstOrDefault(item => item.Sku == sku);
            if (targetItem == null)
            {
                target.Items.Add(new Item
                {
                    Sku = sku,
                    CurrentStock = quantity
                });
            }
            else
            {
                targetItem.CurrentStock += quantity;
            }

            var sourceEntity = _mapper.Map<WarehouseEntity>(source.GetWarehouseData());
            var targetEntity = _mapper.Map<WarehouseEntity>(target.GetWarehouseData());


            await _warehouseRepository.UpdateAsync(sourceEntity);
            await _warehouseRepository.UpdateAsync(targetEntity);

            return Result<bool>.Success(true);
        }


    }
}

