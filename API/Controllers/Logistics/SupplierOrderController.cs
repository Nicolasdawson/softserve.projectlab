using Microsoft.AspNetCore.Mvc;
using API.Services.Logistics; // Updated to use service interface
using Logistics.Models;
using API.Models.Logistics.LogisticsInterface;

namespace API.Controllers.Logistics
{
    /// <summary>
    /// Controller for managing supplier orders.
    /// </summary>
    [ApiController]
    [Route("api/supplier-orders")] // Use plural naming convention
    public class SupplierOrderController : ControllerBase
    {
        private readonly ISupplierOrderService _supplierOrderService;

        /// <summary>
        /// Initializes a new instance of the <see cref="SupplierOrderController"/> class.
        /// </summary>
        /// <param name="supplierOrderService">The supplier order service.</param>
        public SupplierOrderController(ISupplierOrderService supplierOrderService)
        {
            _supplierOrderService = supplierOrderService;
        }

        /// <summary>
        /// Creates a new supplier order.
        /// </summary>
        [HttpPost]
        public IActionResult CreateSupplierOrder([FromBody] SupplierOrder order)
        {
            var result = _supplierOrderService.AddSupplierOrder(order as ISupplierOrder);
            return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
        }

        /// <summary>
        /// Gets a supplier order by its ID.
        /// </summary>
        [HttpGet("{orderId}")]
        public IActionResult GetSupplierOrderById(int orderId)
        {
            var result = _supplierOrderService.GetSupplierOrderById(orderId);
            return result.IsSuccess ? Ok(result.Data) : NotFound(result.ErrorMessage);
        }

        /// <summary>
        /// Updates an existing supplier order.
        /// </summary>
        [HttpPut("{orderId}")]
        public IActionResult UpdateSupplierOrder(int orderId, [FromBody] SupplierOrder order)
        {
            order.OrderId = orderId;
            var result = _supplierOrderService.UpdateSupplierOrder(order as ISupplierOrder);
            return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
        }

        /// <summary>
        /// Deletes a supplier order by its ID.
        /// </summary>
        [HttpDelete("{orderId}")]
        public IActionResult DeleteSupplierOrder(int orderId)
        {
            var result = _supplierOrderService.DeleteSupplierOrder(orderId);
            //return result.IsSuccess ? NoContent() : NotFound(result.ErrorMessage);
            if (result.IsNoContent)
            {
                return NoContent();  // Returns HTTP 204 No Content
            }
            return result.IsSuccess ? Ok(result.Data) : NotFound(result.ErrorMessage);
        }
    }
}
