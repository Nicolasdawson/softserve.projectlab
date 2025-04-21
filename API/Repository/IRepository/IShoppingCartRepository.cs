using API.Data.Models;
using API.Data.Models.DTOs.ShoppingCart;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Repository.IRepository
{
    public interface IShoppingCartRepository
    {
        Task<IEnumerable<ShoppingCartGetDto>> GetShoppingCartItems(int userId);
        Task<bool> AddToCart(ShoppingCartPostDto shoppingCartPostDto);
        Task<bool> UpdateCart(int productId, int userId, string action);
    }
}
