using Microsoft.AspNetCore.Mvc;
using API.Models.IntAdmin;
using API.Services.Interfaces;
using API.Services;

namespace API.Controllers
{
    /// <summary>
    /// Controller for managing warehouse operations.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class WarehouseController : ControllerBase
    {
        private readonly IWarehouseService _warehouseService;

        /// <summary>
        /// Initializes a new instance of the <see cref="WarehouseController"/> class.
        /// </summary>
        /// <param name="warehouseService">The warehouse service.</param>
        public WarehouseController(IWarehouseService warehouseService)
        {
            _warehouseService = warehouseService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWarehouses()
        {
            var warehouses = await _warehouseService.GetWarehousesAsync();
            return Ok(warehouses);
        }



        /// <summary>
        /// Adds an item to the warehouse.
        /// </summary>
        /// <param name="warehouseId">The warehouse identifier.</param>
        /// <param name="item">The item to add.</param>
        /// <returns>An <see cref="IActionResult"/> representing the result of the operation.</returns>
        [HttpPost]
        [Route("{warehouseId}/add-item")]
        public async Task<IActionResult> AddItem(int warehouseId, [FromBody] Item item)
        {
            if (item == null)
            {
                return BadRequest("Item data is required.");
            }

            var result = await _warehouseService.AddItemToWarehouseAsync(warehouseId, item);

            if (result.IsSuccess)
            {
                return Ok("Item added successfully to the warehouse.");
            }

            return BadRequest(result.ErrorMessage);
        }


        /// <summary>
        /// Removes an item from the warehouse.
        /// </summary>
        /// <param name="warehouseId">The warehouse identifier.</param>
        /// <param name="item">The item to remove.</param>
        /// <returns>An <see cref="IActionResult"/> representing the result of the operation.</returns>
        [HttpDelete("{warehouseId}/remove-item")]
        public IActionResult RemoveItem(int warehouseId, [FromBody] Item item)
        {
            var result = _warehouseService.RemoveItemFromWarehouse(warehouseId, item);
            if (result.IsNoContent)
            {
                return NoContent();  // Returns HTTP 204 No Content
            }
            return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
        }


        /// <summary>
        /// Checks the stock of an item in the warehouse.
        /// </summary>
        /// <param name="warehouseId">The warehouse identifier.</param>
        /// <param name="sku">The SKU of the item.</param>
        /// <returns>An <see cref="IActionResult"/> representing the result of the operation.</returns>
        [HttpGet("{warehouseId}/check-stock/{sku}")]
        public IActionResult CheckStock(int warehouseId, int sku)
        {
            var result = _warehouseService.CheckWarehouseStock(warehouseId, sku);
            return result.IsSuccess ? Ok(result.Data) : NotFound(result.ErrorMessage);
        }

        /// <summary>
        /// Transfers an item from one warehouse to another.
        /// </summary>
        /// <param name="warehouseId">The source warehouse identifier.</param>
        /// <param name="sku">The SKU of the item.</param>
        /// <param name="quantity">The quantity to transfer.</param>
        /// <param name="targetWarehouseId">The target warehouse identifier.</param>
        /// <returns>An <see cref="IActionResult"/> representing the result of the operation.</returns>
        [HttpPost("{warehouseId}/transfer/{sku}/{quantity}/{targetWarehouseId}")]
        public IActionResult TransferItem(int warehouseId, int sku, int quantity, int targetWarehouseId)
        {
            var result = _warehouseService.TransferItem(warehouseId, sku, quantity, targetWarehouseId);
            return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
        }

        /// <summary>
        /// Gets the items with low stock in the warehouse.
        /// </summary>
        /// <param name="warehouseId">The warehouse identifier.</param>
        /// <param name="threshold">The stock threshold.</param>
        /// <returns>An <see cref="IActionResult"/> representing the result of the operation.</returns>
        [HttpGet("{warehouseId}/low-stock/{threshold}")]
        public IActionResult GetLowStockItems(int warehouseId, int threshold)
        {
            var result = _warehouseService.GetLowStockItems(warehouseId, threshold);
            return result.IsSuccess ? Ok(result.Data) : NotFound(result.ErrorMessage);
        }

        /// <summary>
        /// Gets the total inventory value of the warehouse.
        /// </summary>
        /// <param name="warehouseId">The warehouse identifier.</param>
        /// <returns>An <see cref="IActionResult"/> representing the result of the operation.</returns>
        [HttpGet("{warehouseId}/total-value")]
        public IActionResult GetTotalInventoryValue(int warehouseId)
        {
            var result = _warehouseService.CalculateTotalInventoryValue(warehouseId);
            return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
        }

        /// <summary>
        /// Generates an inventory report for the warehouse.
        /// </summary>
        /// <param name="warehouseId">The warehouse identifier.</param>
        /// <returns>An <see cref="IActionResult"/> representing the result of the operation.</returns>
        [HttpGet("{warehouseId}/inventory-report")]
        public IActionResult GetInventoryReport(int warehouseId)
        {
            var result = _warehouseService.GenerateInventoryReport(warehouseId);
            return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
        }
    }
}