using API.Models;
using API.Models.Logistics;
using API.Services.Logistics;
using Microsoft.AspNetCore.Mvc;
using softserve.projectlabs.Shared.DTOs;
using softserve.projectlabs.Shared.Interfaces;

namespace API.Controllers.Logistics
{
    [Route("api/warehouses")]
    [ApiController]
    public class WarehouseController : ControllerBase
    {
        private readonly IWarehouseService _warehouseService;

        public WarehouseController(IWarehouseService warehouseService)
        {
            _warehouseService = warehouseService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetWarehouseById(int id)
        {
            var result = await _warehouseService.GetWarehouseByIdAsync(id);
            return result.IsSuccess ? Ok(result.Data) : NotFound(result.ErrorMessage);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWarehouses()
        {
            var warehouses = await _warehouseService.GetAllWarehousesAsync();
            if (warehouses == null || !warehouses.Any())
                return NotFound("No warehouses found.");

            return Ok(warehouses);
        }


        [HttpPost("add-item")]
        public async Task<IActionResult> AddItemToWarehouse([FromBody] AddItemToWarehouseDto addItemDto)
        {
            var result = await _warehouseService.AddItemToWarehouseAsync(addItemDto.WarehouseId, addItemDto.Sku);
            return result.IsSuccess ? Ok() : BadRequest(result.ErrorMessage);
        }

        [HttpPost]
        public async Task<IActionResult> CreateWarehouse([FromBody] WarehouseDto warehouseDto)
        {
            var result = await _warehouseService.CreateWarehouseAsync(warehouseDto);
            return result.IsSuccess ? CreatedAtAction(nameof(GetWarehouseById), new { id = result.Data.WarehouseId }, result.Data) : BadRequest(result.ErrorMessage);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> SoftDeleteWarehouse(int id)
        {
            var result = await _warehouseService.SoftDeleteWarehouseAsync(id);
            return result.IsSuccess ? NoContent() : NotFound(result.ErrorMessage);
        }

        [HttpPost("{id}/restore")]
        public async Task<IActionResult> UndeleteWarehouse(int id)
        {
            var result = await _warehouseService.UndeleteWarehouseAsync(id);
            return result.IsSuccess ? Ok() : NotFound(result.ErrorMessage);
        }
    }
}
