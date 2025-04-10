using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Services.Logistics;
using API.Models.Logistics.Interfaces;
using softserve.projectlabs.Shared.Utilities;
using softserve.projectlabs.Shared.Interfaces;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierOrderController : ControllerBase
    {
        private readonly ISupplierOrderService _service;

        public SupplierOrderController(ISupplierOrderService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<ISupplierOrder>>> GetAllOrders()
        {
            return Ok(await _service.GetAllSupplierOrdersAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ISupplierOrder>> GetOrderById(int id)
        {
            var order = await _service.GetSupplierOrderByIdAsync(id);
            if (order == null) return NotFound();
            return Ok(order);
        }

        [HttpPost]
        public async Task<ActionResult<ISupplierOrder>> CreateOrder(ISupplierOrder order)
        {
            var newOrder = await _service.AddSupplierOrderAsync(order);
            return CreatedAtAction(nameof(GetOrderById), new { id = newOrder.OrderId }, newOrder);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, ISupplierOrder order)
        {
            if (id != order.OrderId) return BadRequest();
            var updated = await _service.UpdateSupplierOrderAsync(order);
            if (!updated) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var deleted = await _service.DeleteSupplierOrderAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
