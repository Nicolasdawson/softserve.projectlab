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

        public async Task<Result<OrderDto>> GetOrderByIdAsync(int orderId)
        {
            var result = await _orderDomain.GetOrderById(orderId);

            if (!result.IsSuccess)
                return Result<OrderDto>.Failure(result.ErrorMessage);

            var orderData = result.Data.GetOrderData();

            return Result<OrderDto>.Success(orderData);
        }

        public async Task<Result<OrderDto>> RetrieveOrderByCartIdAsync(int cartId)
        {
            var result = await _orderDomain.RetrieveOrderByCartId(cartId);

            if (!result.IsSuccess)
                return Result<OrderDto>.Failure(result.ErrorMessage);

            return Result<OrderDto>.Success(result.Data.GetOrderData());
        }

        public async Task<Result<List<OrderDto>>> GetAllOrdersAsync()
        {
            var result = await _orderDomain.GetAllOrders();

            if (!result.IsSuccess)
                return Result<List<OrderDto>>.Failure(result.ErrorMessage);

            var orderDtos = result.Data.Select(order => _mapper.Map<OrderDto>(order.GetOrderData())).ToList();

            return Result<List<OrderDto>>.Success(orderDtos);
        }

        public async Task<Result<bool>> RetrieveAndSaveAllUnsavedOrdersAsync()
        {
            var result = await _orderDomain.RetrieveAndSaveAllUnsavedOrders();

            if (!result.IsSuccess)
                return Result<bool>.Failure(result.ErrorMessage);

            return Result<bool>.Success(true);
        }

        public async Task<Result<OrderDto>> UpdateOrderAsync(OrderDto orderDto)
        {
            var result = await _orderDomain.UpdateOrderFromCart(orderDto.OrderId);

            if (!result.IsSuccess)
                return Result<OrderDto>.Failure(result.ErrorMessage);

            var updatedOrderData = result.Data.GetOrderData();

            return Result<OrderDto>.Success(updatedOrderData);
        }

        public async Task<Result<bool>> DeleteOrderAsync(int orderId)
        {
            return await _orderDomain.DeleteOrder(orderId);
        }

        public async Task<Result<bool>> SaveUnsavedOrdersAsync()
        {
            var result = await _orderDomain.SaveUnsavedOrders();

            if (!result.IsSuccess)
                return Result<bool>.Failure(result.ErrorMessage);

            return Result<bool>.Success(true);
        }

        public async Task<Result<bool>> FulfillOrderAsync(int orderId)
        {
            var order = await _context.OrderEntities
                .Include(o => o.OrderItemEntities)
                .FirstOrDefaultAsync(o => o.OrderId == orderId);

            if (order == null)
                return Result<bool>.Failure("Order not found.");

            var warehouse = new Warehouse(new WarehouseDto()); // Replace with actual warehouse retrieval logic
            var result = await _orderDomain.FulfillOrder(orderId, warehouse);

            if (!result.IsSuccess)
                return Result<bool>.Failure(result.ErrorMessage);

            // Update order status in the database
            order.OrderStatus = "Fulfilled";
            _context.OrderEntities.Update(order);
            await _context.SaveChangesAsync();

            return Result<bool>.Success(true);
        }
    }
}

