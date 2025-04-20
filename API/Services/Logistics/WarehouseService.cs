using API.Implementations.Domain;
using API.Models.IntAdmin;
using API.Data;
using softserve.projectlabs.Shared.Utilities;
using softserve.projectlabs.Shared.Interfaces;
using softserve.projectlabs.Shared.DTOs;
using API.Data.Entities;

public class WarehouseService : IWarehouseService
{
    private readonly ApplicationDbContext _context;
    private readonly WarehouseDomain _warehouseDomain;

    public WarehouseService(WarehouseDomain warehouseDomain, ApplicationDbContext applicationDbContext)
    {
        _context = applicationDbContext;
        _warehouseDomain = warehouseDomain;
    }

    public async Task<List<WarehouseResponseDto>> GetWarehousesAsync()
    {
        var result = await _warehouseDomain.GetAllWarehousesAsync();
        if (!result.IsSuccess)
            return new List<WarehouseResponseDto>();

        var warehouseDtos = result.Data.Select(warehouse =>
        {
            var warehouseData = warehouse.GetWarehouseData();
            return new WarehouseResponseDto
            {
                WarehouseId = warehouseData.WarehouseId,
            };
        }).ToList();

        return warehouseDtos;
    }


    public async Task<Result<WarehouseResponseDto>> GetWarehouseByIdAsync(int warehouseId)
    {
        var result = await _warehouseDomain.GetWarehouseByIdAsync(warehouseId);
        if (!result.IsSuccess)
            return Result<WarehouseResponseDto>.Failure(result.ErrorMessage, result.ErrorCode);

        var warehouseData = result.Data.GetWarehouseData();
        var warehouseDto = new WarehouseResponseDto
        {
            WarehouseId = warehouseData.WarehouseId,
        };

        return Result<WarehouseResponseDto>.Success(warehouseDto);
    }


    public async Task<Result<bool>> AddItemToWarehouseAsync(int warehouseId, AddItemToWarehouseDTO itemDto)
    {
        var result = await _warehouseDomain.AddItemToWarehouseAsync(warehouseId, itemDto);
        return result.IsSuccess
            ? Result<bool>.Success(true)
            : Result<bool>.Failure(result.ErrorMessage, result.ErrorCode);
    }

    public async Task<Result<List<ItemDto>>> GetLowStockItemsAsync(int warehouseId, int threshold)
    {
        var result = await _warehouseDomain.GetWarehouseByIdAsync(warehouseId);
        if (!result.IsSuccess)
            return Result<List<ItemDto>>.Failure(result.ErrorMessage, result.ErrorCode);

        var lowStockResult = await result.Data.GetLowStockItemsAsync(threshold);
        if (!lowStockResult.IsSuccess)
            return Result<List<ItemDto>>.Failure(lowStockResult.ErrorMessage, lowStockResult.ErrorCode);

        var itemDtos = lowStockResult.Data.Select(item => new ItemDto
        {
            ItemId = item.ItemId,
            ItemName = item.ItemName,
            ItemDescription = item.ItemDescription,
            CurrentStock = item.CurrentStock
        }).ToList();

        return Result<List<ItemDto>>.Success(itemDtos);
    }


    public async Task<Result<bool>> RemoveItemFromWarehouseAsync(int warehouseId, int itemId)
    {
        var result = await _warehouseDomain.RemoveItemFromWarehouseAsync(warehouseId, itemId);
        return result.IsSuccess
            ? Result<bool>.Success(true)
            : Result<bool>.Failure(result.ErrorMessage, result.ErrorCode, result.StackTrace);
    }

    public async Task<Result<int>> CheckWarehouseStockAsync(int warehouseId, int sku)
    {
        var result = await _warehouseDomain.GetWarehouseByIdAsync(warehouseId);
        if (!result.IsSuccess)
            return Result<int>.Failure(result.ErrorMessage, result.ErrorCode);

        var stockResult = await result.Data.CheckItemStockAsync(sku);
        return stockResult;
    }


    public async Task<Result<bool>> TransferItemAsync(
     int sourceWarehouseId,
     int sku,
     int quantity,
     int targetWarehouseId)
    {
        var sourceResult = await _warehouseDomain.GetWarehouseByIdAsync(sourceWarehouseId);
        var targetResult = await _warehouseDomain.GetWarehouseByIdAsync(targetWarehouseId);

        if (!sourceResult.IsSuccess || !targetResult.IsSuccess)
            return Result<bool>.Failure("One or both warehouses not found", 404);

        var transferResult = await sourceResult.Data.TransferItemAsync(
            sku,
            quantity,
            targetResult.Data);

        return transferResult;
    }


    public async Task<Result<decimal>> CalculateTotalInventoryValueAsync(int warehouseId)
    {
        var result = await _warehouseDomain.GetWarehouseByIdAsync(warehouseId);
        if (!result.IsSuccess)
            return Result<decimal>.Failure(result.ErrorMessage, result.ErrorCode);

        var valueResult = await result.Data.GetTotalInventoryValueAsync();
        return valueResult;
    }


    public async Task<Result<string>> GenerateInventoryReportAsync(int warehouseId)
    {
        var result = await _warehouseDomain.GetWarehouseByIdAsync(warehouseId);
        if (!result.IsSuccess)
            return Result<string>.Failure(result.ErrorMessage, result.ErrorCode);

        var reportResult = await result.Data.GenerateInventoryReportAsync();
        return reportResult;
    }


    public async Task<Result<bool>> DeleteWarehouseAsync(int warehouseId)
    {
        return await _warehouseDomain.SoftDeleteWarehouseAsync(warehouseId);
    }

    public async Task<Result<bool>> UndeleteWarehouseAsync(int warehouseId)
    {
        return await _warehouseDomain.UndeleteWarehouseAsync(warehouseId);
    }

    public async Task<Result<bool>> CreateWarehouseAsync(WarehouseDto warehouseDto)
    {
        var existingWarehouse = await _warehouseDomain.GetWarehouseByNameAsync(warehouseDto.Name);
        if (existingWarehouse != null)
        {
            return Result<bool>.Failure($"A warehouse with the name '{warehouseDto.Name}' already exists.");
        }

        var warehouseEntity = new WarehouseEntity
        {
            WarehouseLocation = warehouseDto.Name,
            WarehouseCapacity = warehouseDto.Capacity,
            IsDeleted = false 
        };

        var result = await _warehouseDomain.CreateWarehouseAsync(warehouseDto);

        return result.IsSuccess
            ? Result<bool>.Success(true)
            : Result<bool>.Failure(result.ErrorMessage, result.ErrorCode);
    }
}
