using API.Models.Customers;
using API.Models.IntAdmin;
using softserve.projectlabs.Shared.Utilities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Services.Interfaces
{
    public interface ICartService
    {
        Task<Result<Cart>> CreateCartAsync(int customerId);
        Task<Result<Cart>> GetCartByIdAsync(string cartId);
        Task<Result<Cart>> GetCartByCustomerIdAsync(int customerId);
        Task<Result<Cart>> AddItemToCartAsync(string cartId, int itemSku, int quantity);
        Task<Result<Cart>> RemoveItemFromCartAsync(string cartId, int itemSku, int quantity);
        Task<Result<Cart>> ClearCartAsync(string cartId);
        Task<Result<bool>> DeleteCartAsync(string cartId);
        Task<Result<decimal>> GetCartTotalAsync(string cartId);
    }
}