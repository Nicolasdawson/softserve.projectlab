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

        // Example: Add an item to the warehouse
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

        // Example: Remove an item from the warehouse
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

        // Example: Get available stock for a specific SKU
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
