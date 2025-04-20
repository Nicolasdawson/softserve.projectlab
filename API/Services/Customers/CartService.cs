using API.Implementations.Domain;
using API.Models.Customers;
using API.Models.IntAdmin;
using API.Services.Interfaces;
using softserve.projectlabs.Shared.Utilities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Services
{
    public class CartService : ICartService
    {
        private readonly CartDomain _cartDomain;

        public CartService(CartDomain cartDomain)
        {
            _cartDomain = cartDomain;
        }

        public async Task<Result<Cart>> CreateCartAsync(int customerId)
        {
            return await _cartDomain.CreateCartAsync(customerId);
        }

        public async Task<Result<Cart>> GetCartByIdAsync(string cartId)
        {
            return await _cartDomain.GetCartByIdAsync(cartId);
        }

        public async Task<Result<Cart>> GetCartByCustomerIdAsync(int customerId)
        {
            return await _cartDomain.GetCartByCustomerIdAsync(customerId);
        }

        public async Task<Result<Cart>> AddItemToCartAsync(string cartId, int itemSku, int quantity)
        {
            return await _cartDomain.AddItemToCartAsync(cartId, itemSku, quantity);
        }

        public async Task<Result<Cart>> RemoveItemFromCartAsync(string cartId, int itemSku, int quantity)
        {
            return await _cartDomain.RemoveItemFromCartAsync(cartId, itemSku, quantity);
        }

        public async Task<Result<Cart>> ClearCartAsync(string cartId)
        {
            return await _cartDomain.ClearCartAsync(cartId);
        }

        public async Task<Result<bool>> DeleteCartAsync(string cartId)
        {
            return await _cartDomain.DeleteCartAsync(cartId);
        }

        public async Task<Result<decimal>> GetCartTotalAsync(string cartId)
        {
            return await _cartDomain.GetCartTotalAsync(cartId);
        }
    }
}