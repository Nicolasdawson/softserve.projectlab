using Microsoft.AspNetCore.Mvc;
using API.Models.IntAdmin;
using API.Models;
using softserve.projectlabs.Shared.DTOs;
using softserve.projectlabs.Shared.Interfaces;
using softserve.projectlabs.Shared.Utilities;

namespace API.Controllers
{
    [ApiController]
    [Route("api/Warehouse")]
    public class WarehouseController : ControllerBase
    {
        private readonly IWarehouseService _warehouseService;

        public WarehouseController(IWarehouseService warehouseService)
        {
            _warehouseService = warehouseService;
        }

        [HttpGet("{warehouseId}")]
        public async Task<IActionResult> GetWarehouseById(int warehouseId)
        {
            var result = await _warehouseService.GetWarehouseByIdAsync(warehouseId);
            return result.IsSuccess ? Ok(result.Data) : NotFound(result.ErrorMessage);
        }

        [HttpGet("")]
        [ProducesResponseType(typeof(List<WarehouseResponseDto>), 200)]
        public async Task<ActionResult<List<WarehouseResponseDto>>> GetAllWarehouses()
        {
            var warehouses = await _warehouseService.GetWarehousesAsync();

            // Manually map List<WarehouseResponseDto>
            var result = warehouses.Select(w => new WarehouseResponseDto
            {
                WarehouseId = w.WarehouseId,
                Name = w.Name,
                Location = w.Location,
                Capacity = w.Capacity,
                Items = w.Items.Select(i => new ItemDto
                {
                    Sku = i.Sku,
                    ItemName = i.ItemName,
                    ItemDescription = i.ItemDescription,
                    ItemPrice = i.ItemPrice,
                    CurrentStock = i.CurrentStock
                }).ToList(),
                BranchId = w.BranchId
            }).ToList();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateWarehouse([FromBody] WarehouseDto warehouseDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _warehouseService.CreateWarehouseAsync(warehouseDto);

            if (result.IsSuccess)
            {
                return Ok(new { Message = "Warehouse successfully created." });
            }
            else
            {
                return BadRequest(result.ErrorMessage);
            }
        }

        [HttpPost("{warehouseId}/items")]
        public async Task<IActionResult> AddItem(int warehouseId, [FromBody] AddItemToWarehouseDTO itemDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _warehouseService.AddItemToWarehouseAsync(warehouseId, itemDto);

            if (result.IsSuccess)
            {
                return Ok(new { Message = "Item successfully linked to the warehouse." });
            }
            else
            {
                return BadRequest(result.ErrorMessage);
            }
        }

        [HttpDelete("{warehouseId}/items/{itemId}")]
        public async Task<IActionResult> RemoveItem(int warehouseId, int itemId)
        {
            var result = await _warehouseService.RemoveItemFromWarehouseAsync(warehouseId, itemId);
            return result.IsSuccess ? Ok(new { Message = "Item successfully removed." }) : BadRequest(result.ErrorMessage);
        }

        [HttpGet("{warehouseId}/items/{itemId}/stock")]
        public async Task<IActionResult> CheckStock(int warehouseId, int itemId)
        {
            var result = await _warehouseService.CheckWarehouseStockAsync(warehouseId, itemId);
            return result.IsSuccess
                ? Ok(new { ItemId = itemId, Stock = result.Data })
                : NotFound(result.ErrorMessage);
        }

        [HttpPost("transfer")]
        public async Task<IActionResult> TransferItem([FromBody] TransferRequestDto transferRequest)
        {
            var result = await _warehouseService.TransferItemAsync(
                transferRequest.SourceWarehouseId,
                transferRequest.ItemId,
                transferRequest.Quantity,
                transferRequest.TargetWarehouseId);

            return result.IsSuccess
                ? Ok(new { Message = "Transfer completed successfully" })
                : BadRequest(result.ErrorMessage);
        }

        [HttpGet("{warehouseId}/low-stock")]
        public async Task<IActionResult> GetLowStockItems(int warehouseId, [FromQuery] int threshold = 5)
        {
            var result = await _warehouseService.GetLowStockItemsAsync(warehouseId, threshold);
            return result.IsSuccess
                ? Ok(result.Data)
                : NotFound(result.ErrorMessage);
        }

        [HttpGet("{warehouseId}/inventory-value")]
        public async Task<IActionResult> GetTotalInventoryValue(int warehouseId)
        {
            var result = await _warehouseService.CalculateTotalInventoryValueAsync(warehouseId);
            return result.IsSuccess
                ? Ok(new { TotalValue = result.Data })
                : BadRequest(result.ErrorMessage);
        }

        [HttpGet("{warehouseId}/inventory-report")]
        public async Task<IActionResult> GetInventoryReport(int warehouseId)
        {
            var result = await _warehouseService.GenerateInventoryReportAsync(warehouseId);
            return result.IsSuccess
                ? Ok(result.Data)
                : BadRequest(result.ErrorMessage);
        }

        [HttpDelete("{warehouseId}")]
        public async Task<IActionResult> DeleteWarehouse(int warehouseId)
        {
            var result = await _warehouseService.DeleteWarehouseAsync(warehouseId);

            if (result.IsSuccess)
            {
                return Ok(new { Message = "Warehouse successfully deleted." });
            }
            else
            {
                return BadRequest(result.ErrorMessage);
            }
        }

        [HttpPost("{warehouseId}/undelete")]
        public async Task<IActionResult> UndeleteWarehouse(int warehouseId)
        {
            var result = await _warehouseService.UndeleteWarehouseAsync(warehouseId);

            if (result.IsSuccess)
            {
                return Ok(new { Message = "Warehouse successfully restored." });
            }
            else
            {
                return BadRequest(result.ErrorMessage);
            }
        }
    }
}
