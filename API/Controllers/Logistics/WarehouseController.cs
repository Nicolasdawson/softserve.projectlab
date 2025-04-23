using Microsoft.AspNetCore.Mvc;
using API.Models.IntAdmin;
using API.Models;
using softserve.projectlabs.Shared.DTOs;
using softserve.projectlabs.Shared.Interfaces;
using softserve.projectlabs.Shared.Utilities;

namespace API.Controllers
{
    /// <summary>
    /// Controller for managing warehouse operations.
    /// </summary>
    [ApiController]
    [Route("api/Warehouse")]
    public class WarehouseController : ControllerBase
    {
        private readonly IWarehouseService _warehouseService;

        /// <summary>
        /// Initializes a new instance of the <see cref="WarehouseController"/> class.
        /// </summary>
        /// <param name="warehouseService">Service for warehouse operations.</param>
        public WarehouseController(IWarehouseService warehouseService)
        {
            _warehouseService = warehouseService;
        }

        /// <summary>
        /// Retrieves a warehouse by its ID.
        /// </summary>
        /// <param name="warehouseId">The ID of the warehouse.</param>
        /// <returns>The warehouse details if found, otherwise a NotFound result.</returns>
        [HttpGet("{warehouseId}")]
        public async Task<IActionResult> GetWarehouseById(int warehouseId)
        {
            var result = await _warehouseService.GetWarehouseByIdAsync(warehouseId);
            return result.IsSuccess ? Ok(result.Data) : NotFound(result.ErrorMessage);
        }

        /// <summary>
        /// Retrieves all warehouses.
        /// </summary>
        /// <returns>A list of all warehouses.</returns>
        [HttpGet("")]
        [ProducesResponseType(typeof(List<WarehouseResponseDto>), 200)]
        public async Task<ActionResult<List<WarehouseResponseDto>>> GetAllWarehouses()
        {
            var warehouses = await _warehouseService.GetWarehousesAsync();

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

        /// <summary>
        /// Creates a new warehouse.
        /// </summary>
        /// <param name="warehouseDto">The warehouse details.</param>
        /// <returns>A success message if created, otherwise a BadRequest result.</returns>
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

        /// <summary>
        /// Adds an item to a warehouse.
        /// </summary>
        /// <param name="warehouseId">The ID of the warehouse.</param>
        /// <param name="itemDto">The item details.</param>
        /// <returns>A success message if added, otherwise a BadRequest result.</returns>
        [HttpPost("{warehouseId}/items")]
        public async Task<IActionResult> AddItem(int warehouseId, [FromBody] int sku)
        {
            var result = await _warehouseService.AddItemToWarehouseAsync(warehouseId, sku);

            if (result.IsSuccess)
            {
                return Ok(new { Message = "Item successfully linked to the warehouse." });
            }
            else
            {
                return BadRequest(result.ErrorMessage);
            }
        }

        /// <summary>
        /// Removes an item from a warehouse.
        /// </summary>
        /// <param name="warehouseId">The ID of the warehouse.</param>
        /// <param name="itemId">The ID of the item to remove.</param>
        /// <returns>A success message if removed, otherwise a BadRequest result.</returns>
        [HttpDelete("{warehouseId}/items/{itemId}")]
        public async Task<IActionResult> RemoveItem(int warehouseId, int itemId)
        {
            var result = await _warehouseService.RemoveItemFromWarehouseAsync(warehouseId, itemId);
            return result.IsSuccess ? Ok(new { Message = "Item successfully removed." }) : BadRequest(result.ErrorMessage);
        }

        /// <summary>
        /// Checks the stock of an item in a warehouse.
        /// </summary>
        /// <param name="warehouseId">The ID of the warehouse.</param>
        /// <param name="itemId">The ID of the item.</param>
        /// <returns>The stock quantity if found, otherwise a NotFound result.</returns>
        [HttpGet("{warehouseId}/items/{sku}/stock")]
        public async Task<IActionResult> CheckStock(int warehouseId, int sku)
        {
            var result = await _warehouseService.CheckWarehouseStockAsync(warehouseId, sku);
            return result.IsSuccess
                ? Ok(new { Sku = sku, Stock = result.Data })
                : NotFound(result.ErrorMessage);
        }

        /// <summary>
        /// Transfers an item between warehouses.
        /// </summary>
        /// <param name="transferRequest">The transfer request details.</param>
        /// <returns>A success message if transferred, otherwise a BadRequest result.</returns>
        [HttpPost("transfer")]
        public async Task<IActionResult> TransferItem([FromBody] TransferRequestDto transferRequest)
        {
            var result = await _warehouseService.TransferItemAsync(
                transferRequest.SourceWarehouseId,
                transferRequest.Sku,
                transferRequest.Quantity,
                transferRequest.TargetWarehouseId);

            return result.IsSuccess
                ? Ok(new { Message = "Transfer completed successfully" })
                : BadRequest(result.ErrorMessage);
        }

        /// <summary>
        /// Retrieves items with low stock in a warehouse.
        /// </summary>
        /// <param name="warehouseId">The ID of the warehouse.</param>
        /// <param name="threshold">The stock threshold to consider as low.</param>
        /// <returns>A list of low-stock items if found, otherwise a NotFound result.</returns>
        [HttpGet("{warehouseId}/low-stock")]
        public async Task<IActionResult> GetLowStockItems(int warehouseId, [FromQuery] int threshold = 5)
        {
            var result = await _warehouseService.GetLowStockItemsAsync(warehouseId, threshold);
            return result.IsSuccess
                ? Ok(result.Data)
                : NotFound(result.ErrorMessage);
        }

        /// <summary>
        /// Calculates the total inventory value of a warehouse.
        /// </summary>
        /// <param name="warehouseId">The ID of the warehouse.</param>
        /// <returns>The total inventory value if successful, otherwise a BadRequest result.</returns>
        [HttpGet("{warehouseId}/inventory-value")]
        public async Task<IActionResult> GetTotalInventoryValue(int warehouseId)
        {
            var result = await _warehouseService.CalculateTotalInventoryValueAsync(warehouseId);
            return result.IsSuccess
                ? Ok(new { TotalValue = result.Data })
                : BadRequest(result.ErrorMessage);
        }

        /// <summary>
        /// Generates an inventory report for a warehouse.
        /// </summary>
        /// <param name="warehouseId">The ID of the warehouse.</param>
        /// <returns>The inventory report if successful, otherwise a BadRequest result.</returns>
        [HttpGet("{warehouseId}/inventory-report")]
        public async Task<IActionResult> GetInventoryReport(int warehouseId)
        {
            var result = await _warehouseService.GenerateInventoryReportAsync(warehouseId);
            return result.IsSuccess
                ? Ok(result.Data)
                : BadRequest(result.ErrorMessage);
        }

        /// <summary>
        /// Deletes a warehouse.
        /// </summary>
        /// <param name="warehouseId">The ID of the warehouse to delete.</param>
        /// <returns>A success message if deleted, otherwise a BadRequest result.</returns>
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

        /// <summary>
        /// Restores a previously deleted warehouse.
        /// </summary>
        /// <param name="warehouseId">The ID of the warehouse to restore.</param>
        /// <returns>A success message if restored, otherwise a BadRequest result.</returns>
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
