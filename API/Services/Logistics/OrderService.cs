using API.Implementations.Domain;
using API.Models.Logistics;
using API.Data.Entities;
using API.Models;
using API.Data;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using softserve.projectlabs.Shared.Utilities;
using softserve.projectlabs.Shared.Interfaces;
using softserve.projectlabs.Shared.DTOs;

namespace API.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly OrderDomain _orderDomain;

        public OrderService(ApplicationDbContext context, IMapper mapper, OrderDomain orderDomain)
        {
            _context = context;
            _mapper = mapper;
            _orderDomain = orderDomain;
        }

        public async Task<Result<OrderDto>> CreateOrderAsync(OrderItemRequestDto orderRequestDto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var orderDto = new OrderDto
                {
                    OrderDate = DateTime.UtcNow,
                    TotalAmount = 0,
                    Items = new List<OrderItemDto>()
                };

                var itemsToUpdate = new List<ItemEntity>();

                foreach (var itemRequest in orderRequestDto.Items)
                {
                    var itemEntity = await _context.ItemEntities
                        .FirstOrDefaultAsync(i => i.Sku.ToString() == itemRequest.ItemId.ToString());

                    if (itemEntity == null)
                        return Result<OrderDto>.Failure($"Item SKU {itemRequest.ItemId} not found");

                    if (itemEntity.CurrentStock < itemRequest.Quantity)
                        return Result<OrderDto>.Failure($"Insufficient stock for SKU {itemRequest.ItemId}");

                    itemEntity.CurrentStock -= itemRequest.Quantity;
                    itemsToUpdate.Add(itemEntity);

                    orderDto.TotalAmount += itemEntity.ItemPrice * itemRequest.Quantity;
                    orderDto.Items.Add(new OrderItemDto
                    {
                        ItemId = itemEntity.Sku,
                        ItemName = itemEntity.ItemName,
                        Quantity = itemRequest.Quantity,
                        UnitPrice = itemEntity.ItemPrice,
                        // Remove assignment to TotalPrice as it is read-only
                    });
                }

                var order = new Order(orderDto);
                var orderEntity = _mapper.Map<OrderEntity>(order.GetOrderData());

                _context.OrderEntities.Add(orderEntity);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return Result<OrderDto>.Success(order.GetOrderData());
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return Result<OrderDto>.Failure($"Order creation failed: {ex.Message}");
            }
        }



        public async Task<Result<OrderDto>> GetOrderByIdAsync(int orderId)
        {
            var result = await _orderDomain.GetOrderById(orderId);

            if (!result.IsSuccess)
                return Result<OrderDto>.Failure(result.ErrorMessage);

            // Use GetOrderData() to retrieve OrderDto
            var orderData = result.Data.GetOrderData();

            return Result<OrderDto>.Success(orderData);
        }



        public async Task<Result<List<OrderDto>>> GetAllOrdersAsync()
        {
            var result = await _orderDomain.GetAllOrders();

            if (!result.IsSuccess)
                return Result<List<OrderDto>>.Failure(result.ErrorMessage);

            // Use GetOrderData() to map List<Order> to List<OrderDto>
            var orderDtos = result.Data.Select(order => order.GetOrderData()).ToList();

            return Result<List<OrderDto>>.Success(orderDtos);
        }



        public async Task<Result<OrderDto>> UpdateOrderAsync(OrderDto orderDto)
        {
            // Create a new Order instance using OrderDto
            var order = new Order(orderDto);

            var result = await _orderDomain.UpdateOrder(order);

            if (!result.IsSuccess)
                return Result<OrderDto>.Failure(result.ErrorMessage);

            // Use GetOrderData() to retrieve OrderDto
            var updatedOrderData = result.Data.GetOrderData();

            return Result<OrderDto>.Success(updatedOrderData);
        }



        public async Task<Result<bool>> DeleteOrderAsync(int orderId)
        {
            return await _orderDomain.DeleteOrder(orderId);
        }
    }
}

