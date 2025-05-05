using API.Models.Logistics;
using API.Data.Entities;
using API.Models;
using softserve.projectlabs.Shared.Utilities;
using softserve.projectlabs.Shared.DTOs;
using API.Data;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Implementations.Domain
{
    public class OrderDomain
    {
        private readonly List<Order> _orders = new List<Order>();

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

        public async Task<Result<List<Order>>> GetAllOrders(ApplicationDbContext context, IMapper mapper)
        {
            try
            {
                // Query the database for all orders
                var orderEntities = await context.OrderEntities
                    .Include(o => o.OrderItemEntities) 
                    .ToListAsync();

                // Map OrderEntity to Order domain model
                var orders = orderEntities.Select(orderEntity =>
                {
                    var orderDto = mapper.Map<OrderDto>(orderEntity);
                    return new Order(orderDto);
                }).ToList();

                return Result<List<Order>>.Success(orders);
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

        public async Task<Result<Order>> RetrieveOrderByCartId(int cartId, CartEntity cart)
        {
            try
            {
                foreach (var item in cart.CartItemEntities)
                {
                    if (item.SkuNavigation == null)
                    {
                        Console.WriteLine($"Warning: SkuNavigation is null for CartItemEntity with Sku: {item.Sku}");
                    }
                }

                var orderDto = new OrderDto
                {
                    OrderDate = DateTime.UtcNow,
                    TotalAmount = cart.CartItemEntities
                        .Where(item => item.SkuNavigation != null) 
                        .Sum(item => item.SkuNavigation.ItemPrice * item.ItemQuantity),
                    Items = cart.CartItemEntities
                        .Where(item => item.SkuNavigation != null) 
                        .Select(item => new OrderItemDto
                        {
                            Sku = item.Sku,
                            ItemId = item.SkuNavigation.ItemId,
                            ItemName = item.SkuNavigation.ItemName,
                            Quantity = item.ItemQuantity,
                            UnitPrice = item.SkuNavigation.ItemPrice
                        }).ToList(),
                    OrderStatus = "Pending" 
                };

                var order = new Order(orderDto);
                _orders.Add(order);

                return Result<Order>.Success(order);
            }
            catch (Exception ex)
            {
                return Result<Order>.Failure($"Failed to retrieve order: {ex.Message}");
            }
        }

        public async Task<Result<bool>> FulfillOrder(int orderId, Warehouse warehouse)
        {
            try
            {
                var order = _orders.FirstOrDefault(o => o.GetOrderData().OrderId == orderId);

                if (order == null)
                    return Result<bool>.Failure("Order not found.");

                // Deduct stock from the warehouse
                foreach (var item in order.GetOrderData().Items)
                {
                    var result = await warehouse.ReserveStockForOrderAsync(item.Sku, item.Quantity);
                    if (!result.IsSuccess)
                        return Result<bool>.Failure($"Failed to reserve stock for SKU {item.Sku}: {result.ErrorMessage}");
                }

                // Update order status
                order.GetOrderData().OrderStatus = "Fulfilled";

                return Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure($"Failed to fulfill order: {ex.Message}");
            }
        }

    }
}
