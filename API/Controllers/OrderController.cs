using Microsoft.AspNetCore.Mvc;
using API.Services;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly OrderService _orderService;

    public OrderController(OrderService orderService)
    {
        _orderService = orderService;
    }

    // Este endpoint crea la orden a partir del carrito del cliente
    [HttpPost("create-from-cart")]
    public async Task<IActionResult> CreateOrderFromCart([FromQuery] int customerId, [FromQuery] Guid deliveryAddressId)
    {
        try
        {
            var orderResponse = await _orderService.CreateOrderFromCartAsync(customerId, deliveryAddressId);
            return Ok(orderResponse);
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrderById(Guid id)
    {
        var order = await _orderService.GetOrderByIdAsync(id);
        if (order == null) return NotFound();
        return Ok(order);
    }

    [HttpGet("by-customer/{customerId}")]
    public async Task<IActionResult> GetOrdersByCustomer(int customerId)
    {
        var orders = await _orderService.GetOrdersByCustomerIdAsync(customerId);
        return Ok(orders);
    }

    [HttpGet("{id}/details")]
    public async Task<IActionResult> GetOrderDetails(Guid id)
    {
        var orderDto = await _orderService.GetOrderDetailsAsync(id);
        if (orderDto == null) return NotFound();
        return Ok(orderDto);
    }


}

