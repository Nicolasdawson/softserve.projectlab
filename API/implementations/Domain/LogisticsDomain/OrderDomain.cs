using API.Models.Logistics;
using API.Models;

namespace API.Implementations.Domain
{
    public class OrderDomain
    {
        private readonly List<Order> _orders = new List<Order>(); // Example in-memory storage

        public async Task<Result<Order>> CreateOrder(Order order)
        {
            try
            {
                _orders.Add(order);
                return Result<Order>.Success(order);
            }
            catch (Exception ex)
            {
                return Result<Order>.Failure($"Failed to create order: {ex.Message}");
            }
        }

        public async Task<Result<Order>> GetOrderById(int orderId)
        {
            try
            {
                var order = _orders.FirstOrDefault(o => o.OrderId == orderId);
                return order != null ? Result<Order>.Success(order) : Result<Order>.Failure("Order not found.");
            }
            catch (Exception ex)
            {
                return Result<Order>.Failure($"Failed to retrieve order: {ex.Message}");
            }
        }

        public async Task<Result<List<Order>>> GetAllOrders()
        {
            try
            {
                return Result<List<Order>>.Success(_orders);
            }
            catch (Exception ex)
            {
                return Result<List<Order>>.Failure($"Failed to retrieve orders: {ex.Message}");
            }
        }

        public async Task<Result<Order>> UpdateOrder(Order order)
        {
            try
            {
                var existingOrder = _orders.FirstOrDefault(o => o.OrderId == order.OrderId);
                if (existingOrder != null)
                {
                    existingOrder.CustomerName = order.CustomerName;
                    existingOrder.TotalAmount = order.TotalAmount;
                    // Update other properties as necessary
                    return Result<Order>.Success(existingOrder);
                }
                else
                {
                    return Result<Order>.Failure("Order not found.");
                }
            }
            catch (Exception ex)
            {
                return Result<Order>.Failure($"Failed to update order: {ex.Message}");
            }
        }

        public async Task<Result<bool>> DeleteOrder(int orderId)
        {
            try
            {
                var orderToDelete = _orders.FirstOrDefault(o => o.OrderId == orderId);
                if (orderToDelete != null)
                {
                    _orders.Remove(orderToDelete);
                    return Result<bool>.Success(true);
                }
                else
                {
                    return Result<bool>.Failure("Order not found.");
                }
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure($"Failed to delete order: {ex.Message}");
            }
        }
    }
}
