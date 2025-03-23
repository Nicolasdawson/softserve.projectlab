using API.Implementations.Domain;
using API.Models.Logistics;
using API.Models;
using API.Services.Logistics;

namespace API.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly OrderDomain _orderDomain;

        // Constructor Injection - DI will inject the OrderDomain implementation
        public OrderService(OrderDomain orderDomain)
        {
            _orderDomain = orderDomain;
        }

        public async Task<Result<Order>> CreateOrderAsync(Order order)
        {
            return await _orderDomain.CreateOrder(order);
        }

        public async Task<Result<Order>> GetOrderByIdAsync(int orderId)
        {
            return await _orderDomain.GetOrderById(orderId);
        }

        public async Task<Result<List<Order>>> GetAllOrdersAsync()
        {
            return await _orderDomain.GetAllOrders();
        }

        public async Task<Result<Order>> UpdateOrderAsync(Order order)
        {
            return await _orderDomain.UpdateOrder(order);
        }

        public async Task<Result<bool>> DeleteOrderAsync(int orderId)
        {
            return await _orderDomain.DeleteOrder(orderId);
        }
    }
}
