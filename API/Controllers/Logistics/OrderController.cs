using API.Models.Logistics;
using API.Services.Logistics;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using softserve.projectlabs.Shared.Utilities;
using softserve.projectlabs.Shared.Interfaces;
using softserve.projectlabs.Shared.DTOs;
using AutoMapper; // Add this for mapping

namespace API.Controllers.Logistics
{
    /// <summary>
    /// Controller for managing orders in the logistics system.
    /// </summary>
    [Route("api/orders")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderController"/> class.
        /// </summary>
        /// <param name="orderService">The service for handling order operations.</param>
        /// <param name="mapper">The mapper for DTO conversions.</param>
        public OrderController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieves an order by its ID.
        /// </summary>
        /// <param name="id">The ID of the order to retrieve.</param>
        /// <returns>An <see cref="IActionResult"/> containing the order details or an error message.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var result = await _orderService.GetOrderByIdAsync(id);
            return result.IsSuccess ? Ok(result.Data) : NotFound(result.ErrorMessage);
        }

        /// <summary>
        /// Retrieves all orders.
        /// </summary>
        /// <returns>An <see cref="IActionResult"/> containing a list of all orders or an error message.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var result = await _orderService.GetAllOrdersAsync();
            return result.IsSuccess ? Ok(result.Data) : NotFound(result.ErrorMessage);
        }

        /// <summary>
        /// Updates an existing order.
        /// </summary>
        /// <param name="id">The ID of the order to update.</param>
        /// <param name="order">The updated order details.</param>
        /// <returns>An <see cref="IActionResult"/> indicating the result of the operation.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, [FromBody] Order order)
        {
            var orderDto = _mapper.Map<OrderDto>(order);
            orderDto.OrderId = id;
            var result = await _orderService.UpdateOrderAsync(orderDto);
            return result.IsSuccess ? Ok(result.Data) : NotFound(result.ErrorMessage);
        }

        /// <summary>
        /// Deletes an order by its ID.
        /// </summary>
        /// <param name="id">The ID of the order to delete.</param>
        /// <returns>An <see cref="IActionResult"/> indicating the result of the operation.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var result = await _orderService.DeleteOrderAsync(id);
            if (result.IsNoContent)
            {
                return NoContent();
            }
            return result.IsSuccess ? Ok(result.Data) : NotFound(result.ErrorMessage);
        }

        /// <summary>
        /// Retrieves an order by its associated cart ID.
        /// </summary>
        /// <param name="cartId">The ID of the cart associated with the order.</param>
        /// <returns>An <see cref="IActionResult"/> containing the order details or an error message.</returns>
        [HttpGet("cart/{cartId}")]
        public async Task<IActionResult> RetrieveOrderByCartId(int cartId)
        {
            var result = await _orderService.RetrieveOrderByCartIdAsync(cartId);
            return result.IsSuccess ? Ok(result.Data) : NotFound(result.ErrorMessage);
        }

        /// <summary>
        /// Marks an order as fulfilled.
        /// </summary>
        /// <param name="orderId">The ID of the order to fulfill.</param>
        /// <returns>An <see cref="IActionResult"/> indicating the result of the operation.</returns>
        [HttpPost("{orderId}/fulfill")]
        public async Task<IActionResult> FulfillOrder(int orderId)
        {
            var result = await _orderService.FulfillOrderAsync(orderId);
            return result.IsSuccess ? Ok("Order fulfilled successfully.") : BadRequest(result.ErrorMessage);
        }
    }
}


