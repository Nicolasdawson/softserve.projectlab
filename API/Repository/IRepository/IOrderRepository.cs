using API.Data.Models;
using API.Data.Models.DTOs.Order;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Repository.IRepository
{
    public interface IOrderRepository
    {
        Task<IEnumerable<OrderGetAllDto>> GetAllOrders();
        Task<IEnumerable<OrderGetByUserDto>> GetOrdersByUser(int userId);
        Task<IEnumerable<object>> GetOrderDetail(int orderId);
        Task<bool> PlaceOrder(OrderPostDto orderPostDto);
    }
}
