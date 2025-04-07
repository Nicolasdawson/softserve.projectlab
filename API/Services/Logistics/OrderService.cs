using API.Implementations.Domain;
using API.Models.Logistics;
using API.Services.Logistics;
using API.Data.Entities;
using API.Models;
using API.Data;
using AutoMapper;
using Microsoft.EntityFrameworkCore;


namespace API.Services.OrderService
{   
    /// <summary>
    /// Service class for managing orders.
    /// </summary>
    public class OrderService : IOrderService
    {

        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly OrderDomain _orderDomain;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderService"/> class.
        /// </summary>
        /// <param name="orderDomain">The order domain implementation.</param>
        public OrderService(OrderDomain orderDomain)
        {
            _orderDomain = orderDomain;
        }

        public OrderService(ApplicationDbContext context, IMapper mapper, OrderDomain orderDomain)
        {
            _context = context;
            _mapper = mapper;
            _orderDomain = orderDomain;
        }

        public async Task<Result<Order>> CreateOrderAsync(OrderItemRequest orderRequest)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // 1. Map request to entity
                var orderEntity = _mapper.Map<OrderEntity>(orderRequest);
                decimal totalAmount = 0;
                var itemsToUpdate = new List<ItemEntity>();

                // 2. Process items
                foreach (var itemRequest in orderRequest.Items)
                {
                    var itemEntity = await _context.ItemEntities
                        .FirstOrDefaultAsync(i => i.Sku.ToString() == itemRequest.Sku);

                    if (itemEntity == null)
                        return Result<Order>.Failure($"Item SKU {itemRequest.Sku} not found");

                    if (itemEntity.CurrentStock < itemRequest.Quantity)
                        return Result<Order>.Failure($"Insufficient stock for SKU {itemRequest.Sku}");

                    // Update stock
                    itemEntity.CurrentStock -= itemRequest.Quantity;
                    itemsToUpdate.Add(itemEntity);

                    // Calculate total
                    totalAmount += itemEntity.ItemPrice * itemRequest.Quantity;
                }

                // 3. Finalize order
                orderEntity.OrderTotalAmount = totalAmount;
                orderEntity.OrderDate = DateTime.UtcNow;
                orderEntity.OrderStatus = "Pending";

                _context.OrderEntities.Add(orderEntity);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                // 4. Return mapped response
                return Result<Order>.Success(_mapper.Map<Order>(orderEntity));
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return Result<Order>.Failure($"Order creation failed: {ex.Message}");
            }
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
