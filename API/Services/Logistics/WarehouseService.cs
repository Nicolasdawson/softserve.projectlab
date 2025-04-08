using API.Implementations.Domain;
using API.Models.IntAdmin;
using API.Models.Logistics.Interfaces;
using API.Models.Logistics;
using API.Models;
using API.Services.Interfaces;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using API.Data.Entities;
using API.Data;

public class WarehouseService : IWarehouseService
{
    private readonly ApplicationDbContext _context;
    private readonly WarehouseDomain _warehouseDomain;
    private readonly IMapper _mapper;

    public WarehouseService(WarehouseDomain warehouseDomain, IMapper mapper, ApplicationDbContext applicationDbContext)
    {
        _context = applicationDbContext;
        _warehouseDomain = warehouseDomain;
        _mapper = mapper;
    }

    public async Task<List<Warehouse>> GetWarehousesAsync()
    {
        var result = await _warehouseDomain.GetAllWarehousesAsync();
        return result.IsSuccess ? result.Data : new List<Warehouse>();
    }

    public async Task<Result<Warehouse>> GetWarehouseByIdAsync(int warehouseId)
    {
        return await _warehouseDomain.GetWarehouseByIdAsync(warehouseId);
    }

    public async Task<Result<IWarehouse>> AddItemToWarehouseAsync(int warehouseId, Item item)
    {
        var strategy = _context.Database.CreateExecutionStrategy();

        return await strategy.ExecuteAsync(async () =>
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var warehouseResult = await GetWarehouseByIdAsync(warehouseId);
                if (!warehouseResult.IsSuccess)
                    return Result<IWarehouse>.Failure(warehouseResult.ErrorMessage, warehouseResult.ErrorCode);

                if (item == null)
                    return Result<IWarehouse>.Failure("Item cannot be null", 400);

                // Check if item exists in system
                var itemEntity = await _context.ItemEntities
                    .FirstOrDefaultAsync(i => i.Sku == item.Sku)
                    ?? _mapper.Map<ItemEntity>(item);

                if (itemEntity.Sku == 0) // New item
                {
                    await _context.ItemEntities.AddAsync(itemEntity);
                    await _context.SaveChangesAsync();
                }

                // Add warehouse-item relationship
                var warehouseItem = new WarehouseItemEntity
                {
                    WarehouseId = warehouseId,
                    Sku = itemEntity.Sku,
                    ItemQuantity = item.CurrentStock
                };

                await _context.WarehouseItemEntities.AddAsync(warehouseItem);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                // Map the result to IWarehouse
                var warehouse = _mapper.Map<IWarehouse>(warehouseResult.Data);
                return Result<IWarehouse>.Success(warehouse);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return Result<IWarehouse>.Failure(
                    $"Failed to add item: {ex.Message}",
                    500,
                    ex.StackTrace);
            }
        });
    }



    public async Task<Result<bool>> RemoveItemFromWarehouseAsync(int warehouseId, int itemId)
    {
        var result = await _warehouseDomain.RemoveItemFromWarehouseAsync(warehouseId, itemId);
        if (result.IsNoContent)
            return Result<bool>.NoContent();

        return result.IsSuccess
            ? Result<bool>.Success(true)
            : Result<bool>.Failure(result.ErrorMessage, result.ErrorCode, result.StackTrace);
    }

    public async Task<Result<int>> CheckWarehouseStockAsync(int warehouseId, int sku)
    {
        var result = await _warehouseDomain.GetWarehouseByIdAsync(warehouseId);
        if (!result.IsSuccess)
            return Result<int>.Failure(result.ErrorMessage, result.ErrorCode, result.StackTrace);

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

    public async Task<Result<List<Item>>> GetLowStockItemsAsync(int warehouseId, int threshold)
    {
        var result = await _warehouseDomain.GetWarehouseByIdAsync(warehouseId);
        if (!result.IsSuccess)
            return Result<List<Item>>.Failure(result.ErrorMessage, result.ErrorCode, result.StackTrace);

        var lowStockResult = await result.Data.GetLowStockItemsAsync(threshold);
        return lowStockResult;
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
}