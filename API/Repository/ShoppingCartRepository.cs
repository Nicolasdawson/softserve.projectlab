using API.Data;
using API.Data.Models;
using API.Data.Models.DTOs.ShoppingCart;
using API.Repository.IRepository;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly ApplicationDbContext db;
        private readonly IMapper mapper;

        public ShoppingCartRepository(ApplicationDbContext db, IMapper mapper)
        {
            this.mapper = mapper;
            this.db = db;
        }


        public async Task<bool> AddToCart(ShoppingCartPostDto shoppingCartPostDto)
        {
            var product = await db.Product.FindAsync(shoppingCartPostDto.ProductId);
            if (product == null)
            {
                return false;
            }

            //check if item is already in the cart
            var existingCarItem = await db.ShoppingCartItem
                .FirstOrDefaultAsync(s => s.ProductId == shoppingCartPostDto.ProductId && s.UserId == shoppingCartPostDto.UserId);
            if (existingCarItem != null)
            {
                //Item already exists, update the quantity and total amount
                existingCarItem.Qty += shoppingCartPostDto.Qty;
                existingCarItem.TotalAmount = existingCarItem.Price * existingCarItem.Qty;
            }

            else
            {
                var ShoppingCartItemNew = mapper.Map<ShoppingCartItem>(shoppingCartPostDto);
                //Item does not exist, add new item to the cart
                ShoppingCartItemNew.Price = product.Price;
                ShoppingCartItemNew.TotalAmount = product.Price * shoppingCartPostDto.Qty;  

                await db.ShoppingCartItem.AddAsync(ShoppingCartItemNew);
            }

            await db.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<ShoppingCartGetDto>> GetShoppingCartItems(int userId)
        {
            var ShoppingCart = await (from shoppingCarItems in db.ShoppingCartItem.Where(s => s.UserId == userId)
                                      join products in db.Product on shoppingCarItems.ProductId equals products.Id
                                      select new ShoppingCartGetDto 
                                      {
                                          productId = products.Id,
                                          productName = products.Name,
                                          ImageUrl = products.ImageUrl,
                                          Price = shoppingCarItems.Price,
                                          TotalAmount = shoppingCarItems.TotalAmount,
                                          Qty = shoppingCarItems.Qty
                                      }).ToListAsync();

            return ShoppingCart;
        }

        public async Task<bool> UpdateCart(int productId, int userId, string action)
        {
            var existingCarItem = await db.ShoppingCartItem.FirstOrDefaultAsync(s => s.ProductId == productId && s.UserId == userId);
            if (existingCarItem == null)
            {
                return false;
            }

            switch (action)
            {
                case "add":
                    existingCarItem.Qty += 1;
                    existingCarItem.TotalAmount = existingCarItem.Price * existingCarItem.Qty;
                    break;
                case "subtract":
                    if (existingCarItem.Qty > 1)
                    {
                        existingCarItem.Qty -= 1;
                        existingCarItem.TotalAmount = existingCarItem.Price * existingCarItem.Qty;
                    }
                    else
                    {
                        db.ShoppingCartItem.Remove(existingCarItem);
                    }
                    break;
                case "delete":
                    db.ShoppingCartItem.Remove(existingCarItem);
                    break;
                default: return false;
            }

            await db.SaveChangesAsync();
            return true;
        }
    }
}
