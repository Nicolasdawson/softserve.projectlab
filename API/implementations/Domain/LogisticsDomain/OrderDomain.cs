using API.Models.Logistics;
using API.Data.Entities;
using softserve.projectlabs.Shared.Utilities;
using softserve.projectlabs.Shared.DTOs;
using AutoMapper;
using API.Data;
using API.Data.Repositories.LogisticsRepositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Implementations.Domain
{
    public class OrderDomain
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        private readonly WarehouseDomain _warehouseDomain;

        public OrderDomain(IOrderRepository orderRepository, IMapper mapper, ApplicationDbContext context, WarehouseDomain warehouseDomain)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _context = context;
            _warehouseDomain = warehouseDomain;
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

        public async Task<Result<List<Order>>> GetAllOrders()
        {
            try
            {
                var orderEntities = await _context.OrderEntities
                    .Include(o => o.Customer)
                    .Include(o => o.Customer.CartEntities)
                    .ThenInclude(c => c.CartItemEntities)
                    .ThenInclude(ci => ci.SkuNavigation)
                    .ToListAsync();

                var orders = orderEntities.Select(orderEntity =>
                {
                    var orderDto = new OrderDto
                    {
                        OrderId = orderEntity.OrderId,
                        CustomerId = orderEntity.CustomerId,
                        OrderDate = orderEntity.OrderDate,
                        TotalAmount = orderEntity.OrderTotalAmount,
                        OrderStatus = orderEntity.OrderStatus,
                        Items = orderEntity.Customer.CartEntities
                            .SelectMany(c => c.CartItemEntities)
                            .Select(ci => new OrderItemDto
                            {
                                ItemId = ci.SkuNavigation.ItemId,
                                Quantity = ci.ItemQuantity
                            }).ToList()
                    };

                    return new Order(orderDto);
                }).ToList();

                return Result<List<Order>>.Success(orders);
            }
            catch (Exception ex)
            {
                return Result<List<Order>>.Failure($"Failed to retrieve orders: {ex.Message}");
            }
        }

        public async Task<Result<Order>> UpdateOrderFromCart(int orderId)
        {
            try
            {
                // Retrieve the existing order
                var orderEntity = await _context.OrderEntities
                    .Include(o => o.OrderItemEntities)
                    .FirstOrDefaultAsync(o => o.OrderId == orderId);

                if (orderEntity == null)
                    return Result<Order>.Failure("Order not found.");

                // Retrieve the associated cart
                var cart = await _context.CartEntities
                    .Include(c => c.CartItemEntities)
                    .ThenInclude(ci => ci.SkuNavigation)
                    .FirstOrDefaultAsync(c => c.CustomerId == orderEntity.CustomerId);

                if (cart == null)
                    return Result<Order>.Failure("Associated cart not found.");

                // Update the order to match the cart
                orderEntity.OrderTotalAmount = cart.CartItemEntities.Sum(ci => ci.SkuNavigation.ItemPrice * ci.ItemQuantity);
                orderEntity.UpdatedAt = DateTime.UtcNow;

                // Update order items
                foreach (var cartItem in cart.CartItemEntities)
                {
                    var existingOrderItem = orderEntity.OrderItemEntities
                        .FirstOrDefault(oi => oi.Sku == cartItem.SkuNavigation.ItemId);

                    if (existingOrderItem != null)
                    {
                        // Update existing order item
                        existingOrderItem.ItemQuantity = cartItem.ItemQuantity;
                    }
                    else
                    {
                        // Add new order item
                        var newOrderItem = new OrderItemEntity
                        {
                            OrderId = orderEntity.OrderId,
                            Sku = cartItem.SkuNavigation.ItemId,
                            ItemQuantity = cartItem.ItemQuantity
                        };
                        orderEntity.OrderItemEntities.Add(newOrderItem);
                    }
                }

                // Save changes to the database
                _context.OrderEntities.Update(orderEntity);
                await _context.SaveChangesAsync();

                var updatedOrderDto = _mapper.Map<OrderDto>(orderEntity);
                var updatedOrder = new Order(updatedOrderDto);

                return Result<Order>.Success(updatedOrder);
            }
            catch (Exception ex)
            {
                return Result<Order>.Failure($"Failed to update order from cart: {ex.Message}");
            }
        }

        public async Task<Result<bool>> RetrieveAndSaveAllUnsavedOrders()
        {
            // Use the execution strategy provided by the DbContext
            var strategy = _context.Database.CreateExecutionStrategy();

            return await strategy.ExecuteAsync(async () =>
            {
                using var transaction = await _context.Database.BeginTransactionAsync();
                try
                {
                    var carts = await _context.CartEntities
                        .Include(c => c.CartItemEntities)
                        .ThenInclude(ci => ci.SkuNavigation)
                        .Where(c => !_context.OrderEntities.Any(o => o.CustomerId == c.CustomerId))
                        .ToListAsync();

                    foreach (var cart in carts)
                    {
                        // Create and save the OrderEntity
                        var orderEntity = new OrderEntity
                        {
                            CustomerId = cart.CustomerId,
                            OrderDate = DateTime.UtcNow,
                            OrderTotalAmount = cart.CartItemEntities.Sum(ci => ci.SkuNavigation.ItemPrice * ci.ItemQuantity),
                            OrderStatus = "Pending",
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        };

                        await _context.OrderEntities.AddAsync(orderEntity);
                    }

                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return Result<bool>.Success(true);
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    return Result<bool>.Failure($"Failed to retrieve and save unsaved orders: {ex.Message}");
                }
            });
        }

        private async Task<OrderEntity> CreateAndSaveOrderEntityAsync(CartEntity cart)
        {
            // Create OrderEntity
            var orderEntity = new OrderEntity
            {
                CustomerId = cart.CustomerId,
                OrderDate = DateTime.UtcNow,
                OrderTotalAmount = cart.CartItemEntities.Sum(ci => ci.SkuNavigation.ItemPrice * ci.ItemQuantity),
                OrderStatus = "Pending",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            // Save the OrderEntity to the database
            await _context.OrderEntities.AddAsync(orderEntity);
            await _context.SaveChangesAsync();

            // Validate that the OrderId was generated
            if (orderEntity.OrderId <= 0)
            {
                throw new InvalidOperationException("Failed to generate a valid OrderId.");
            }

            return orderEntity;
        }

        private async Task CreateAndSaveOrderItemsAsync(OrderEntity orderEntity, ICollection<CartItemEntity> cartItems)
        {
            foreach (var cartItem in cartItems)
            {
                // Check if the Sku exists in the ItemEntity table
                var itemExists = await _context.ItemEntities.AnyAsync(i => i.ItemId == cartItem.SkuNavigation.ItemId);
                if (!itemExists)
                {
                    throw new InvalidOperationException($"Item with Sku {cartItem.SkuNavigation.ItemId} does not exist in the ItemEntity table.");
                }

                var existingOrderItem = await _context.OrderItemEntities
                    .FirstOrDefaultAsync(oi => oi.OrderId == orderEntity.OrderId && oi.Sku == cartItem.SkuNavigation.ItemId);

                if (existingOrderItem == null)
                {
                    var orderItemEntity = new OrderItemEntity
                    {
                        OrderId = orderEntity.OrderId,
                        Sku = cartItem.SkuNavigation.ItemId,
                        ItemQuantity = cartItem.ItemQuantity
                    };
                    Console.WriteLine($"Adding new OrderItemEntity: OrderId={orderItemEntity.OrderId}, Sku={orderItemEntity.Sku}, Quantity={orderItemEntity.ItemQuantity}");
                    await _context.OrderItemEntities.AddAsync(orderItemEntity);
                }
                else
                {
                    existingOrderItem.ItemQuantity += cartItem.ItemQuantity;
                    Console.WriteLine($"Updating existing OrderItemEntity: OrderId={existingOrderItem.OrderId}, Sku={existingOrderItem.Sku}, NewQuantity={existingOrderItem.ItemQuantity}");
                    _context.Entry(existingOrderItem).State = EntityState.Modified;
                }
            }

            await _context.SaveChangesAsync();
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

        public async Task<Result<Order>> RetrieveOrderByCartId(int cartId)
        {
            try
            {
                // Retrieve the cart
                var cart = await _context.CartEntities
                    .Include(c => c.CartItemEntities)
                    .ThenInclude(ci => ci.SkuNavigation)
                    .FirstOrDefaultAsync(c => c.CartId == cartId);

                if (cart == null)
                    return Result<Order>.Failure("Cart not found.");

                // Check if the customer exists
                var customerExists = await _context.CustomerEntities.AnyAsync(c => c.CustomerId == cart.CustomerId);
                if (!customerExists)
                    return Result<Order>.Failure($"Customer with ID {cart.CustomerId} does not exist.");

                // Create OrderDto
                var orderDto = new OrderDto
                {
                    OrderId = 0, // New order, ID will be generated
                    CustomerId = cart.CustomerId,
                    OrderDate = DateTime.UtcNow,
                    TotalAmount = cart.CartItemEntities.Sum(ci => ci.SkuNavigation.ItemPrice * ci.ItemQuantity),
                    OrderStatus = "Pending",
                    Items = cart.CartItemEntities.Select(ci => new OrderItemDto
                    {
                        ItemId = ci.SkuNavigation.ItemId,
                        Quantity = ci.ItemQuantity
                    }).ToList()
                };

                // Create Order using OrderDto
                var order = new Order(orderDto);

                return Result<Order>.Success(order);
            }
            catch (Exception ex)
            {
                return Result<Order>.Failure($"Failed to retrieve order: {ex.Message}");
            }
        }

        public async Task<Result<bool>> SaveUnsavedOrders()
        {
            try
            {
                var carts = await _context.CartEntities
                    .Include(c => c.CartItemEntities)
                    .ThenInclude(ci => ci.SkuNavigation)
                    .ToListAsync();

                foreach (var cart in carts)
                {
                    var existingOrder = await _context.OrderEntities
                        .FirstOrDefaultAsync(o => o.CustomerId == cart.CustomerId && !o.IsDeleted);

                    if (existingOrder == null)
                    {
                        var orderDto = new OrderDto
                        {
                            OrderId = 0,
                            CustomerId = cart.CustomerId,
                            OrderDate = DateTime.UtcNow,
                            TotalAmount = cart.CartItemEntities.Sum(ci => ci.SkuNavigation.ItemPrice * ci.ItemQuantity),
                            OrderStatus = "Pending",
                            Items = cart.CartItemEntities.Select(ci => new OrderItemDto
                            {
                                ItemId = ci.SkuNavigation.ItemId,
                                Quantity = ci.ItemQuantity
                            }).ToList()
                        };

                        var order = new Order(orderDto);
                        var orderEntity = _mapper.Map<OrderEntity>(order.GetOrderData());
                        await _context.OrderEntities.AddAsync(orderEntity);
                        await _context.SaveChangesAsync();

                        foreach (var cartItem in cart.CartItemEntities)
                        {
                            var orderItemEntity = new OrderItemEntity
                            {
                                OrderId = orderEntity.OrderId,
                                Sku = cartItem.SkuNavigation.ItemId,
                                ItemQuantity = cartItem.ItemQuantity
                            };
                            await _context.OrderItemEntities.AddAsync(orderItemEntity);
                        }
                        await _context.SaveChangesAsync();
                    }
                }

                return Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure($"Failed to save unsaved orders: {ex.Message}");
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
                    var result = await _warehouseDomain.ReserveStockForOrderAsync(warehouse.GetWarehouseData().WarehouseId, item.Sku, item.Quantity);

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
