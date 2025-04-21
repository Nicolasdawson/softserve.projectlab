using API.Data.Models;
using API.Data.Models.DTOs.ShoppingCart;
using API.Repository;
using API.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartItemsController : ControllerBase
    {
        private readonly IShoppingCartRepository shoppingCartRepository;

        public ShoppingCartItemsController(IShoppingCartRepository shoppingCartRepository)
        {
            this.shoppingCartRepository = shoppingCartRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ShoppingCartPostDto shoppingCartPostDto)
        {
            var isAdded = await shoppingCartRepository.AddToCart(shoppingCartPostDto);

            if (isAdded)
            {
                return StatusCode(StatusCodes.Status201Created);
            }
            else
            {
                return BadRequest("Item could not be added to the cart");
            }
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> Get(int userId)
        {
            var shoppingCarItems = await shoppingCartRepository.GetShoppingCartItems(userId);
            if (shoppingCarItems.Any())
            {
                return Ok(shoppingCarItems);
            }
            else
            {
                return NotFound("No items found in the shopping cart");
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put(int productId, int userId, string action)
        {
            var isUpdated = await shoppingCartRepository.UpdateCart(productId, userId, action);
            if (isUpdated)
            {
                return Ok("Cart updated successfully");
            }
            else
            {
                return BadRequest("Item could not be updated in the cart");
            }
        }
    }
}
