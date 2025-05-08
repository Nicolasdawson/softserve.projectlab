using API.Models.Logistics;
using API.Data.Entities;
using API.Repositories.LogisticsRepositories.Interfaces;
using softserve.projectlabs.Shared.Utilities;
using softserve.projectlabs.Shared.DTOs;
using AutoMapper;
using API.Data;

namespace API.Implementations.Domain
{
    public class OrderDomain
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public OrderDomain(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<Result<Order>> GetOrderById(int orderId)
        {
            try
            {
                var orderEntity = await _orderRepository.GetByIdAsync(orderId);
                if (orderEntity == null)
                    return Result<Order>.Failure("Order not found.");

                var orderDto = _mapper.Map<OrderDto>(orderEntity);
                var order = new Order(orderDto);

                return Result<Order>.Success(order);
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
                var orderEntities = await _orderRepository.GetAllAsync();
                var orders = orderEntities.Select(orderEntity =>
                {
                    var orderDto = _mapper.Map<OrderDto>(orderEntity);
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
                var orderEntity = _mapper.Map<OrderEntity>(order.GetOrderData());
                await _orderRepository.UpdateAsync(orderEntity);

                return Result<Order>.Success(order);
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
                await _orderRepository.DeleteAsync(orderId);
                return Result<bool>.Success(true);
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
                var orderEntity = await _orderRepository.GetByIdAsync(orderId);
                if (orderEntity == null)
                    return Result<bool>.Failure("Order not found.");

                var orderDto = _mapper.Map<OrderDto>(orderEntity);
                var order = new Order(orderDto);

                foreach (var item in order.GetOrderData().Items)
                {
                    var result = await warehouse.ReserveStockForOrderAsync(item.Sku, item.Quantity);
                    if (!result.IsSuccess)
                        return Result<bool>.Failure($"Failed to reserve stock for SKU {item.Sku}: {result.ErrorMessage}");
                }

                order.GetOrderData().OrderStatus = "Fulfilled";

                // Update the order status in the database
                orderEntity.OrderStatus = "Fulfilled";
                await _orderRepository.UpdateAsync(orderEntity);

                return Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure($"Failed to fulfill order: {ex.Message}");
            }
        }
    }
}
