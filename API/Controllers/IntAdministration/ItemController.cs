using API.Models.IntAdmin;
using API.Services.IntAdmin;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers.IntAdmin
{
    /// <summary>
    /// API Controller for managing Item operations.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;

        /// <summary>
        /// Constructor with dependency injection for IItemService.
        /// </summary>
        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }

        /// <summary>
        /// Adds a new item.
        /// </summary>
        /// <param name="item">Item object to add</param>
        /// <returns>HTTP response with the created item or error message</returns>
        [HttpPost("add")]
        public async Task<IActionResult> AddItem([FromBody] Item item)
        {
            var result = await _itemService.AddItemAsync(item);
            return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
        }

        /// <summary>
        /// Updates an existing item.
        /// </summary>
        /// <param name="item">Item object with updated data</param>
        /// <returns>HTTP response with the updated item or error message</returns>
        [HttpPut("update")]
        public async Task<IActionResult> UpdateItem([FromBody] Item item)
        {
            var result = await _itemService.UpdateItemAsync(item);
            return result.IsSuccess ? Ok(result.Data) : NotFound(result.ErrorMessage);
        }

        /// <summary>
        /// Retrieves an item by its SKU.
        /// </summary>
        /// <param name="sku">Unique identifier of the item</param>
        /// <returns>HTTP response with the item or error message</returns>
        [HttpGet("{sku}")]
        public async Task<IActionResult> GetItemBySku(int sku)
        {
            var result = await _itemService.GetItemBySkuAsync(sku);
            return result.IsSuccess ? Ok(result.Data) : NotFound(result.ErrorMessage);
        }

        /// <summary>
        /// Retrieves all items.
        /// </summary>
        /// <returns>HTTP response with the list of items or error message</returns>
        [HttpGet("all")]
        public async Task<IActionResult> GetAllItems()
        {
            var result = await _itemService.GetAllItemsAsync();
            return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
        }

        /// <summary>
        /// Removes an item by its SKU.
        /// </summary>
        /// <param name="sku">Unique identifier of the item to remove</param>
        /// <returns>HTTP response indicating success or failure</returns>
        [HttpDelete("remove/{sku}")]
        public async Task<IActionResult> RemoveItem(int sku)
        {
            var result = await _itemService.RemoveItemAsync(sku);
            return result.IsSuccess ? Ok(result.Data) : NotFound(result.ErrorMessage);
        }

        /// <summary>
        /// Updates the price of an item.
        /// </summary>
        /// <param name="sku">Unique identifier of the item</param>
        /// <returns>HTTP response indicating success or failure</returns>
        [HttpPut("update-price/{sku}")]
        public async Task<IActionResult> UpdatePrice(int sku)
        {
            var result = await _itemService.UpdatePriceAsync(sku);
            return result.IsSuccess ? Ok("Price updated successfully.") : BadRequest(result.ErrorMessage);
        }

        /// <summary>
        /// Updates the discount of an item.
        /// </summary>
        /// <param name="sku">Unique identifier of the item</param>
        /// <returns>HTTP response indicating success or failure</returns>
        [HttpPut("update-discount/{sku}")]
        public async Task<IActionResult> UpdateDiscount(int sku)
        {
            var result = await _itemService.UpdateDiscountAsync(sku);
            return result.IsSuccess ? Ok("Discount updated successfully.") : BadRequest(result.ErrorMessage);
        }
    }
}
