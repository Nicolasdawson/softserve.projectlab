using API.Models.Customers;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers.Customers
{
    [ApiController]
    [Route("api/carts")]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        /// <summary>
        /// Crea un nuevo carrito para un cliente.
        /// </summary>
        /// <param name="customerId">ID del cliente</param>
        /// <returns>El carrito creado</returns>
        [HttpPost("customer/{customerId}")]
        public async Task<IActionResult> CreateCart(int customerId)
        {
            var result = await _cartService.CreateCartAsync(customerId);
            return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
        }

        /// <summary>
        /// Obtiene un carrito por su ID.
        /// </summary>
        /// <param name="id">ID del carrito</param>
        /// <returns>El carrito encontrado</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCartById(string id)
        {
            var result = await _cartService.GetCartByIdAsync(id);
            return result.IsSuccess ? Ok(result.Data) : NotFound(result.ErrorMessage);
        }

        /// <summary>
        /// Obtiene el carrito de un cliente por su ID.
        /// </summary>
        /// <param name="customerId">ID del cliente</param>
        /// <returns>El carrito del cliente</returns>
        [HttpGet("customer/{customerId}")]
        public async Task<IActionResult> GetCartByCustomerId(int customerId)
        {
            var result = await _cartService.GetCartByCustomerIdAsync(customerId);
            return result.IsSuccess ? Ok(result.Data) : NotFound(result.ErrorMessage);
        }

        /// <summary>
        /// Añade un item al carrito.
        /// </summary>
        /// <param name="id">ID del carrito</param>
        /// <param name="itemSku">SKU del item</param>
        /// <param name="quantity">Cantidad a añadir</param>
        /// <returns>El carrito actualizado</returns>
        [HttpPost("{id}/items/{itemSku}")]
        public async Task<IActionResult> AddItemToCart(string id, int itemSku, [FromQuery] int quantity = 1)
        {
            var result = await _cartService.AddItemToCartAsync(id, itemSku, quantity);
            return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
        }

        /// <summary>
        /// Elimina un item del carrito.
        /// </summary>
        /// <param name="id">ID del carrito</param>
        /// <param name="itemSku">SKU del item</param>
        /// <param name="quantity">Cantidad a eliminar</param>
        /// <returns>El carrito actualizado</returns>
        [HttpDelete("{id}/items/{itemSku}")]
        public async Task<IActionResult> RemoveItemFromCart(string id, int itemSku, [FromQuery] int quantity = 1)
        {
            var result = await _cartService.RemoveItemFromCartAsync(id, itemSku, quantity);
            return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
        }

        /// <summary>
        /// Vacía el carrito.
        /// </summary>
        /// <param name="id">ID del carrito</param>
        /// <returns>El carrito vacío</returns>
        [HttpDelete("{id}/clear")]
        public async Task<IActionResult> ClearCart(string id)
        {
            var result = await _cartService.ClearCartAsync(id);
            return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
        }

        /// <summary>
        /// Elimina un carrito.
        /// </summary>
        /// <param name="id">ID del carrito</param>
        /// <returns>Resultado de la operación</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCart(string id)
        {
            var result = await _cartService.DeleteCartAsync(id);
            return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
        }

        /// <summary>
        /// Obtiene el total del carrito.
        /// </summary>
        /// <param name="id">ID del carrito</param>
        /// <returns>El total del carrito</returns>
        [HttpGet("{id}/total")]
        public async Task<IActionResult> GetCartTotal(string id)
        {
            var result = await _cartService.GetCartTotalAsync(id);
            return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
        }
    }
}