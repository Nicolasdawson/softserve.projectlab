using API.Implementations.Domain;
using API.Models;
using API.Models.Logistics;
using softserve.projectlabs.Shared.DTOs;
using softserve.projectlabs.Shared.Interfaces;
using softserve.projectlabs.Shared.Utilities;

namespace API.Services.Logistics
{
    public class WarehouseService : IWarehouseService
    {
        private readonly WarehouseDomain _warehouseDomain;

        public WarehouseService(WarehouseDomain warehouseDomain)
        {
            _warehouseDomain = warehouseDomain;
        }

        public async Task<List<WarehouseResponseDto>> GetAllWarehousesAsync()
        {
            var result = await _warehouseDomain.GetAllWarehousesAsync();
            if (!result.IsSuccess)
                return new List<WarehouseResponseDto>();

            return result.Data.Select(warehouse =>
            {
                var warehouseData = warehouse.GetWarehouseData();
                return new WarehouseResponseDto
                {
                    WarehouseId = warehouseData.WarehouseId,
                    Name = warehouseData.Name,
                    Location = warehouseData.Location,
                    Capacity = warehouseData.Capacity,
                    BranchId = warehouseData.BranchId,
                    Items = warehouse.Items.Select(item => new ItemDto
                    {
                        ItemId = item.ItemId,
                        Sku = item.Sku,
                        ItemName = item.ItemName,
                        ItemDescription = item.ItemDescription,
                        CurrentStock = item.CurrentStock
                    }).ToList()
                };
            }).ToList();
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
                Name = warehouseData.Name,
                Location = warehouseData.Location,
                Capacity = warehouseData.Capacity,
                BranchId = warehouseData.BranchId,
                Items = result.Data.Items.Select(item => new ItemDto
                {
                    ItemId = item.ItemId,
                    Sku = item.Sku,
                    ItemName = item.ItemName,
                    ItemDescription = item.ItemDescription,
                    CurrentStock = item.CurrentStock
                }).ToList()
            };

            return Result<WarehouseResponseDto>.Success(warehouseDto);
        }

        public async Task<Result<bool>> AddItemToWarehouseAsync(int warehouseId, int sku)
        {
            var addItemDto = new AddItemToWarehouseDto
            {
                WarehouseId = warehouseId,
                Sku = sku,
                CurrentStock = 0 
            };

            return await _warehouseDomain.AddItemToWarehouseAsync(addItemDto);
        }

        public async Task<Result<bool>> RemoveItemFromWarehouseAsync(int warehouseId, int itemId)
        {
            return await _warehouseDomain.RemoveItemFromWarehouseAsync(warehouseId, itemId);
        }

        public async Task<Result<int>> CheckWarehouseStockAsync(int warehouseId, int sku)
        {
            var result = await _warehouseDomain.GetWarehouseByIdAsync(warehouseId);
            if (!result.IsSuccess)
                return Result<int>.Failure(result.ErrorMessage, result.ErrorCode);

            return await result.Data.CheckItemStockAsync(sku);
        }

        public async Task<Result<bool>> TransferItemAsync(int sourceWarehouseId, int sku, int quantity, int targetWarehouseId)
        {
            var sourceResult = await _warehouseDomain.GetWarehouseByIdAsync(sourceWarehouseId);
            var targetResult = await _warehouseDomain.GetWarehouseByIdAsync(targetWarehouseId);

            if (!sourceResult.IsSuccess || !targetResult.IsSuccess)
                return Result<bool>.Failure("One or both warehouses not found", 404);

            return await _warehouseDomain.TransferItemAsync(sourceResult.Data, sku, quantity, targetResult.Data);
        }

        public async Task<Result<List<ItemDto>>> GetLowStockItemsAsync(int warehouseId, int threshold)
        {
            var result = await _warehouseDomain.GetWarehouseByIdAsync(warehouseId);
            if (!result.IsSuccess)
                return Result<List<ItemDto>>.Failure(result.ErrorMessage, result.ErrorCode);

            var lowStockResult = await result.Data.GetLowStockItemsAsync(threshold);
            if (!lowStockResult.IsSuccess)
                return Result<List<ItemDto>>.Failure(lowStockResult.ErrorMessage, lowStockResult.ErrorCode);

            return Result<List<ItemDto>>.Success(lowStockResult.Data.Select(item => new ItemDto
            {
                ItemId = item.ItemId,
                Sku = item.Sku,
                ItemName = item.ItemName,
                ItemDescription = item.ItemDescription,
                CurrentStock = item.CurrentStock
            }).ToList());
        }

        public async Task<Result<decimal>> CalculateTotalInventoryValueAsync(int warehouseId)
        {
            var result = await _warehouseDomain.GetWarehouseByIdAsync(warehouseId);
            if (!result.IsSuccess)
                return Result<decimal>.Failure(result.ErrorMessage, result.ErrorCode);

            return await result.Data.GetTotalInventoryValueAsync();
        }

        public async Task<Result<string>> GenerateInventoryReportAsync(int warehouseId)
        {
            var result = await _warehouseDomain.GetWarehouseByIdAsync(warehouseId);
            if (!result.IsSuccess)
                return Result<string>.Failure(result.ErrorMessage, result.ErrorCode);

            return await result.Data.GenerateInventoryReportAsync();
        }

        public async Task<Result<bool>> DeleteWarehouseAsync(int warehouseId)
        {
            return await _warehouseDomain.SoftDeleteWarehouseAsync(warehouseId);
        }

        public async Task<Result<bool>> UndeleteWarehouseAsync(int warehouseId)
        {
            return await _warehouseDomain.UndeleteWarehouseAsync(warehouseId);
        }

        public async Task<Result<WarehouseResponseDto>> CreateWarehouseAsync(WarehouseDto warehouseDto)
        {
            var result = await _warehouseDomain.CreateWarehouseAsync(warehouseDto);
            if (!result.IsSuccess)
                return Result<WarehouseResponseDto>.Failure(result.ErrorMessage, result.ErrorCode);

            var warehouseData = result.Data.GetWarehouseData();
            var warehouseDtoResponse = new WarehouseResponseDto
            {
                WarehouseId = warehouseData.WarehouseId,
                Name = warehouseData.Name,
                Location = warehouseData.Location,
                Capacity = warehouseData.Capacity,
                BranchId = warehouseData.BranchId
            };

            return Result<WarehouseResponseDto>.Success(warehouseDtoResponse);
        }

        public async Task<Result<bool>> SoftDeleteWarehouseAsync(int warehouseId)
        {
            return await _warehouseDomain.SoftDeleteWarehouseAsync(warehouseId);
        }


    }

}
