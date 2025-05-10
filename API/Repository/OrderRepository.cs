using API.Data;
using API.Data.Models;
using API.Data.Models.DTOs.Order;
using API.Repository.IRepository;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext db;
        private readonly IMapper mapper;

        public OrderRepository(ApplicationDbContext db, IMapper mapper)
        {
            this.mapper = mapper;
            this.db = db;
        }

        public async Task<IEnumerable<OrderGetAllDto>> GetAllOrders()
        {
            var orders = await db.Order.Include(o => o.User).OrderByDescending(order => order.OrderPlaced).Select(order => new OrderGetAllDto
            {
                Id = order.Id,
                Address = order.Address,
                OrderTotal = order.OrderTotal,
                OrderPlaced = order.OrderPlaced,
                UserEmail = order.User.Email
            }).ToListAsync();

            return orders;
        }

        public async Task<IEnumerable<object>> GetOrderDetail(int orderId)
        {
            var orderDetails = await(from orderDetail in db.OrderDetail
                                     join product in db.Product on orderDetail.ProductId equals product.Id
                                     where orderDetail.OrderId == orderId
                                     select new
                                     {
                                         Id = orderDetail.Id,
                                         Qty = orderDetail.Qty,
                                         subTotal = orderDetail.TotalAmount,
                                         ProductName = product.Name,
                                         productImage = product.ImageUrl,
                                         ProductPrice = product.Price,
                                     }).ToListAsync();

            return orderDetails;
        }

        public async Task<IEnumerable<OrderGetByUserDto>> GetOrdersByUser(int userId)
        {
            var orders = await db.Order.Where(order => order.UserId == userId).Select(order => new OrderGetByUserDto
            {
                Id = order.Id,
                Address = order.Address,
                OrderTotal = order.OrderTotal,
                OrderPlaced = order.OrderPlaced,
            }).ToListAsync();

            return orders;
        }

        public async Task<bool> PlaceOrder(OrderPostDto orderPostDto)
        {
            if (orderPostDto == null)
            {
                return false;
            }

            var order = mapper.Map<Order>(orderPostDto);

            // Primero, guarda el Order para que tenga un Id válido
            order.OrderTotal = 0; // temporal
            await db.Order.AddAsync(order);
            await db.SaveChangesAsync(); // Aquí se genera el Order.Id

            var shoppingCarItems = await db.ShoppingCartItem
               .Where(cart => cart.UserId == order.UserId)
               .ToListAsync();

            decimal Amount = 0;

            var orderDetails = new List<OrderDetail>();

            foreach (var item in shoppingCarItems)
            {
                var orderDetail = new OrderDetail
                {
                    Price = item.Price,
                    ProductId = item.ProductId,
                    Qty = item.Qty,
                    TotalAmount = item.TotalAmount,
                    OrderId = order.Id // Ya existe un Id válido
                };

                Amount += orderDetail.TotalAmount;
                orderDetails.Add(orderDetail);
            }

            // Agrega todos los detalles en bloque
            await db.OrderDetail.AddRangeAsync(orderDetails);

            // Actualiza el total del pedido
            order.OrderTotal = Amount;
            db.Order.Update(order); // Marca como modificado

            await db.SaveChangesAsync();

            // Limpia el carrito
            db.ShoppingCartItem.RemoveRange(shoppingCarItems);
            await db.SaveChangesAsync();

            return true;
        }
    }
}
