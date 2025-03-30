using API.Data.Entities;
using API.Models.IntAdmin;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Services.IntAdmin
{
    /// <summary>
    /// Service interface for item operations.
    /// </summary>
    public interface IItemService
    {
        Task<Result<Item>> AddItemAsync(Item item);
        Task<Result<Item>> UpdateItemAsync(Item item);
        Task<Result<Item>> GetItemBySkuAsync(int sku);
        Task<Result<List<Item>>> GetAllItemsAsync();
        Task<Result<bool>> RemoveItemAsync(int sku);
        Task<Result<bool>> UpdatePriceAsync(int sku);
        Task<Result<bool>> UpdateDiscountAsync(int sku);
    }
}
