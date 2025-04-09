using Microsoft.AspNetCore.Mvc;
using API.Models.IntAdmin;
using API.Services.Interfaces;
using System.Threading.Tasks;
using API.Models.Logistics;
using API.Models;
using AutoMapper;
using API.Models.DTOs;
using Microsoft.Extensions.Logging;


namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WarehouseController : ControllerBase
    {
        private readonly IWarehouseService _warehouseService;
        private readonly IMapper _mapper;
        private readonly ILogger<WarehouseController> _logger; // Add ILogger

        public WarehouseController(IWarehouseService warehouseService, IMapper mapper, ILogger<WarehouseController> logger)
        {
            _warehouseService = warehouseService;
            _mapper = mapper;
            _logger = logger; // Use the correctly typed logger
        }

        [HttpGet("{warehouseId}")]
        public async Task<IActionResult> GetWarehouseById(int warehouseId)
        {
            var result = await _warehouseService.GetWarehouseByIdAsync(warehouseId);
            return result.IsSuccess ? Ok(result.Data) : NotFound(result.ErrorMessage);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWarehouses()
        {
            var warehouses = await _warehouseService.GetWarehousesAsync();
            return Ok(warehouses);
        }

        [HttpPost("{warehouseId}/items")]
        public async Task<IActionResult> AddItem(int warehouseId, [FromBody] AddItemToWarehouseDTO itemDto)
        {
            _logger.LogInformation("Starting AddItem for WarehouseId: {WarehouseId}", warehouseId);

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for WarehouseId: {WarehouseId}", warehouseId);
                return BadRequest(ModelState);
            }

            // Business rule validation
            if (itemDto.CurrentStock <= 0)
            {
                _logger.LogWarning("Current stock must be positive. WarehouseId: {WarehouseId}, CurrentStock: {CurrentStock}", warehouseId, itemDto.CurrentStock);
                return BadRequest("Current stock must be positive");
            }

            if (itemDto.UnitCost <= 0)
            {
                _logger.LogWarning("Unit cost must be positive. WarehouseId: {WarehouseId}, UnitCost: {UnitCost}", warehouseId, itemDto.UnitCost);
                return BadRequest("Unit cost must be positive");
            }

            var item = _mapper.Map<Item>(itemDto);
            _logger.LogInformation("Mapped AddItemToWarehouseDTO to Item. SKU: {Sku}, Name: {Name}", item.Sku, item.Name);

            // Calculate final price if not provided
            if (itemDto.ItemPrice == 0)
            {
                item.ItemPrice = CalculateItemPrice(itemDto);
                _logger.LogInformation("Calculated ItemPrice for SKU: {Sku}. Final Price: {ItemPrice}", item.Sku, item.ItemPrice);
            }

            var result = await _warehouseService.AddItemToWarehouseAsync(warehouseId, item);

            if (result.IsSuccess)
            {
                _logger.LogInformation("Successfully added item to WarehouseId: {WarehouseId}, SKU: {Sku}", warehouseId, item.Sku);
                return Ok(new { Message = "Item successfully added to the database." });
            }
            else
            {
                _logger.LogError("Failed to add item to WarehouseId: {WarehouseId}, SKU: {Sku}. Error: {ErrorMessage}", warehouseId, item.Sku, result.ErrorMessage);
                return BadRequest(result.ErrorMessage);
            }
        }


        [HttpDelete("{warehouseId}/items/{itemId}")]
        public async Task<IActionResult> RemoveItem(int warehouseId, int itemId)
        {
            var result = await _warehouseService.RemoveItemFromWarehouseAsync(warehouseId, itemId);

            if (result.IsNoContent)
                return NoContent();

            return result.IsSuccess
                ? Ok(result.Data)
                : BadRequest(result.ErrorMessage);
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

        private decimal CalculateItemPrice(AddItemToWarehouseDTO item)
        {
            return (item.UnitCost * (1 + item.MarginGain))
                   - (item.ItemDiscount ?? 0)
                   + (item.AdditionalTax ?? 0);
        }
    } 

    public class TransferRequestDto
    {
        public int SourceWarehouseId { get; set; }
        public int TargetWarehouseId { get; set; }
        public int ItemId { get; set; }
        public int Quantity { get; set; }
    }
}