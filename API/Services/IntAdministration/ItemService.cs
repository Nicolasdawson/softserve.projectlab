using API.Implementations.Domain;
using API.Models.IntAdmin;
using softserve.projectlabs.Shared.DTOs.Item;
using softserve.projectlabs.Shared.Utilities;
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

        public ItemService(ItemDomain itemDomain)
        {
            _itemDomain = itemDomain;
        }

        public Task<Result<Item>> CreateItemAsync(ItemCreateDto itemDto)
        {
            return _itemDomain.CreateItemAsync(itemDto);
        }

        public Task<Result<Item>> UpdateItemAsync(int itemId, ItemDto itemDto)
        {
            return _itemDomain.UpdateItemAsync(itemId, itemDto);
        }

        public Task<Result<Item>> GetItemByIdAsync(int itemId)
        {
            return _itemDomain.GetItemByIdAsync(itemId);
        }

        public Task<Result<List<Item>>> GetAllItemsAsync()
        {
            return _itemDomain.GetAllItemsAsync();
        }

        public Task<Result<bool>> DeleteItemAsync(int itemId)
        {
            return _itemDomain.DeleteItemAsync(itemId);
        }

        public Task<Result<bool>> UpdatePriceAsync(int itemId, decimal newPrice)
        {
            return _itemDomain.UpdatePriceAsync(itemId, newPrice);
        }

        public Task<Result<bool>> UpdateDiscountAsync(int itemId, decimal? newDiscount)
        {
            return _itemDomain.UpdateDiscountAsync(itemId, newDiscount);
        }
    }
}
