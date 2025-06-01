using Microsoft.AspNetCore.Mvc;
using API.Services;
using API.Models;
using API.DTO.ShoppingCart;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ShoppingCartController : ControllerBase
{
    private readonly ShoppingCartService _shoppingCartService;

    public ShoppingCartController(ShoppingCartService shoppingCartService)
    {
        _shoppingCartService = shoppingCartService;
    }

    //  Agregar ítem al carrito
    [HttpPost("add")]
    public async Task<IActionResult> AddToCart([FromBody] AddToCartRequest request)
    {
        var result = await _shoppingCartService.AddToCartAsync(request.CustomerId, request.ProductId, request.Quantity);
        return Ok(result);
    }

    //  Obtener carrito de un cliente
    [HttpGet("{customerId}")]
    public async Task<IActionResult> GetCart(int customerId)
    {
        var cart = await _shoppingCartService.GetCartByCustomerAsync(customerId);
        return Ok(cart);
    }

    // Actualizar cantidad
    [HttpPut("update")]
    public async Task<IActionResult> UpdateQuantity([FromBody] UpdateCartQuantityRequest request)
    {
        await _shoppingCartService.UpdateQuantityAsync(request.CustomerId, request.ProductId, request.Quantity);
        return NoContent();
    }

    //  Eliminar ítem del carrito
    [HttpDelete("remove/{cartItemId}")]
    public async Task<IActionResult> RemoveItem(Guid cartItemId)
    {
        await _shoppingCartService.RemoveFromCartAsync(cartItemId);
        return NoContent();
    }

    //  Vaciar carrito
    [HttpDelete("clear/{customerId}")]
    public async Task<IActionResult> ClearCart(int customerId)
    {
        await _shoppingCartService.ClearCartAsync(customerId);
        return NoContent();
    }
}
