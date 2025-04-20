using API.Models.Logistics;
using API.Data.Entities;
using API.Models;
using softserve.projectlabs.Shared.Utilities;

namespace API.Implementations.Domain
{
    public class OrderDomain
    {
        private readonly List<Order> _orders = new List<Order>(); // Example in-memory storage

        public async Task<Result<Order>> CreateOrder(Order order)
        {
            try
            {
                // Use GetOrderData() to retrieve OrderDto
                var orderData = order.GetOrderData();

                // Add the order to the in-memory storage
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
                var order = _orders.FirstOrDefault(o => o.GetOrderData().OrderId == orderId);

                return order != null
                    ? Result<Order>.Success(order)
                    : Result<Order>.Failure("Order not found.");
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
                var existingOrder = _orders.FirstOrDefault(o => o.GetOrderData().OrderId == order.GetOrderData().OrderId);

                if (existingOrder != null)
                {
                    // Update the existing order's data
                    var orderData = order.GetOrderData();
                    var existingOrderData = existingOrder.GetOrderData();

                    existingOrderData.TotalAmount = orderData.TotalAmount;
                    existingOrderData.Items = orderData.Items;

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
                var orderToDelete = _orders.FirstOrDefault(o => o.GetOrderData().OrderId == orderId);

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
