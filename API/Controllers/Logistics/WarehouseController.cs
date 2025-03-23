using API.Models.IntAdmin;
using API.Models.Logistics.Interfaces;
using API.Models;
using API.Services.Logistics;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Logistics
{

    [Route("api/[controller]")]
    [ApiController]
    public class WarehouseController : ControllerBase
    {
        private readonly IWarehouseService _warehouseService;

        public WarehouseController(IWarehouseService warehouseService)
        {
            _warehouseService = warehouseService;
        }

        /// <summary>
        /// Adds an item to the warehouse.
        /// </summary>
        /// <param name="item">The item to add.</param>
        /// <returns>A result indicating success or failure.</returns>
        [HttpPost("add-item")]
        public ActionResult<Result<IWarehouse>> AddItem([FromBody] Item item)
        {
            var result = _warehouseService.AddItem(item);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result.ErrorMessage);
        }

        /// <summary>
        /// Removes an item from the warehouse.
        /// </summary>
        /// <param name="item">The item to remove.</param>
        /// <returns>A result indicating success or failure.</returns>
        [HttpDelete("remove-item")]
        public ActionResult<Result<IWarehouse>> RemoveItem([FromBody] Item item)
        {
            var result = _warehouseService.RemoveItem(item);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result.ErrorMessage);
        }

        /// <summary>
        /// Gets the available stock for a given SKU.
        /// </summary>
        /// <param name="sku">The SKU to check.</param>
        /// <returns>A result indicating success or failure.</returns>
        [HttpGet("stock/{sku}")]
        public ActionResult<Result<IWarehouse>> GetAvailableStock(string sku)
        {
            var result = _warehouseService.GetAvailableStock(sku);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return NotFound(result.ErrorMessage);
        }
    }

}
