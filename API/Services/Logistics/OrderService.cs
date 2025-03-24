using API.Implementations.Domain;
using API.Models.Logistics;
using API.Models;
using API.Services.Logistics;

namespace API.Services.OrderService
{
    /// <summary>
    /// Service class for managing orders.
    /// </summary>
    public class OrderService : IOrderService
    {
        private readonly OrderDomain _orderDomain;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderService"/> class.
        /// </summary>
        /// <param name="orderDomain">The order domain implementation.</param>
        public OrderService(OrderDomain orderDomain)
        {
            _orderDomain = orderDomain;
        }

        /// <summary>
        /// Creates a new order asynchronously.
        /// </summary>
        /// <param name="order">The order to create.</param>
        /// <returns>A result containing the created order.</returns>
        public async Task<Result<Order>> CreateOrderAsync(Order order)
        {
            return await _orderDomain.CreateOrder(order);
        }

        /// <summary>
        /// Retrieves an order by its ID asynchronously.
        /// </summary>
        /// <param name="orderId">The ID of the order to retrieve.</param>
        /// <returns>A result containing the order with the specified ID.</returns>
        public async Task<Result<Order>> GetOrderByIdAsync(int orderId)
        {
            return await _orderDomain.GetOrderById(orderId);
        }

        /// <summary>
        /// Retrieves all orders asynchronously.
        /// </summary>
        /// <returns>A result containing a list of all orders.</returns>
        public async Task<Result<List<Order>>> GetAllOrdersAsync()
        {
            return await _orderDomain.GetAllOrders();
        }

        /// <summary>
        /// Updates an existing order asynchronously.
        /// </summary>
        /// <param name="order">The order to update.</param>
        /// <returns>A result containing the updated order.</returns>
        public async Task<Result<Order>> UpdateOrderAsync(Order order)
        {
            return await _orderDomain.UpdateOrder(order);
        }

        /// <summary>
        /// Deletes an order by its ID asynchronously.
        /// </summary>
        /// <param name="orderId">The ID of the order to delete.</param>
        /// <returns>A result indicating whether the deletion was successful.</returns>
        public async Task<Result<bool>> DeleteOrderAsync(int orderId)
        {
            return await _orderDomain.DeleteOrder(orderId);
        }
    }
}
