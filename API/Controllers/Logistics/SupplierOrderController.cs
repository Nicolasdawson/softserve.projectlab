using Microsoft.AspNetCore.Mvc;
using API.Models.Logistics.Interfaces;
using API.Models.Logistics.LogisticsInterface;
using Logistics.Models;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SupplierOrderController : ControllerBase
    {
        private readonly ISupplierOrder _supplierOrderService;

        public SupplierOrderController(ISupplierOrder supplierOrderService)
        {
            _supplierOrderService = supplierOrderService;
        }

        [HttpPost]
        public IActionResult CreateOrder([FromBody] SupplierOrder order)
        {
            var result = _supplierOrderService.AddSupplierOrder(order as ISupplierOrder);
            if (result.IsSuccess)
                return Ok(result.Data);

            return BadRequest(result.ErrorMessage);
        }

        [HttpGet("{orderId}")]
        public IActionResult GetOrder(int orderId)
        {
            var result = _supplierOrderService.GetSupplierOrderById(orderId);
            if (result.IsSuccess)
                return Ok(result.Data);

            return NotFound(result.ErrorMessage);
        }

        [HttpPut("{orderId}")]
        public IActionResult UpdateOrder(int orderId, [FromBody] SupplierOrder order)
        {
            order.OrderId = orderId; // Ensures that the ID in the request matches
            var result = _supplierOrderService.UpdateSupplierOrder(order as ISupplierOrder);
            if (result.IsSuccess)
                return Ok(result.Data);

            return BadRequest(result.ErrorMessage);
        }

        [HttpDelete("{orderId}")]
        public IActionResult DeleteOrder(int orderId)
        {
            var result = _supplierOrderService.DeleteSupplierOrder(orderId);
            if (result.IsSuccess)
                return NoContent();

            return NotFound(result.ErrorMessage);
        }
    }
}
