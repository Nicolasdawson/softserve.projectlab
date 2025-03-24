using API.Implementations.Domain;
using API.Models;
using API.Models.IntAdmin;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Services.IntAdmin
{
    /// <summary>
    /// Service class for item operations. Delegates business logic to the ItemDomain.
    /// </summary>
    public class ItemService : IItemService
    {
        private readonly ItemDomain _itemDomain;

        /// <summary>
        /// Constructor with dependency injection for ItemDomain.
        /// </summary>
        public ItemService(ItemDomain itemDomain)
        {
            _itemDomain = itemDomain;
        }

        public async Task<Result<Item>> AddItemAsync(Item item)
        {
            return await _itemDomain.CreateItem(item);
        }

        public async Task<Result<Item>> UpdateItemAsync(Item item)
        {
            return await _itemDomain.UpdateItem(item);
        }

        public async Task<Result<Item>> GetItemBySkuAsync(int sku)
        {
            return await _itemDomain.GetItemBySku(sku);
        }

        public async Task<Result<List<Item>>> GetAllItemsAsync()
        {
            return await _itemDomain.GetAllItems();
        }

        public async Task<Result<bool>> RemoveItemAsync(int sku)
        {
            return await _itemDomain.RemoveItem(sku);
        }

        public async Task<Result<bool>> UpdatePriceAsync(int sku)
        {
            // TODO: Implement logic to update the price of the item with the given SKU.
            // For now, simply return success.
            return Result<bool>.Success(true);
        }

        public async Task<Result<bool>> UpdateDiscountAsync(int sku)
        {
            // TODO: Implement logic to update the discount of the item with the given SKU.
            // For now, simply return success.
            return Result<bool>.Success(true);
        }
    }
}
