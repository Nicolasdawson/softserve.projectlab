using API.implementations.Infrastructure.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;
using API.DTO.Order;
using API.Abstractions;
using Microsoft.EntityFrameworkCore.Storage;

namespace API.Services;

public class OrderService
{
    private readonly AppDbContext _context;
    private readonly IStockReservationService _stockReservationService;
    private readonly IPaymentService _paymentService;

    public OrderService(
        AppDbContext context,
        IStockReservationService stockReservationService,
        IPaymentService paymentService)
    {
        _context = context;
        _stockReservationService = stockReservationService;
        _paymentService = paymentService;
    }

    public async Task<OrderResponse> CreateOrderFromCartAsync(int customerId, Guid deliveryAddressId)
    {
        var transaction = _context.Database.CurrentTransaction ?? await TryBeginTransactionAsync();

        try
        {
            var customer = await _context.Customers.FindAsync(customerId)
                           ?? throw new Exception("Customer not found");

            var cartItems = await _context.ShoppingCarts
                .Where(c => c.IdCustomer == customerId)
                .Include(c => c.Product)
                .ToListAsync();

            if (cartItems == null || !cartItems.Any())
                throw new Exception("Shopping cart is empty");

            var order = new Order
            {
                Id = Guid.NewGuid(),
                IdCustomer = customerId,
                IdDeliveryAddress = deliveryAddressId,
                Status = "created",
                OrderNumber = GenerateOrderNumber(),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                OrderItems = new List<OrderItem>()
            };

            decimal total = 0;

            foreach (var item in cartItems)
            {
                var success = await _stockReservationService.TryReserveStockAsync(item.IdProduct, item.Quantity);
                if (!success)
                    throw new Exception($"Insufficient stock for product {item.Product.Name}");

                order.OrderItems.Add(new OrderItem
                {
                    Id = Guid.NewGuid(),
                    IdOrder = order.Id,
                    IdProduct = item.IdProduct,
                    Quantity = item.Quantity,
                    Price = item.Product.Price
                });

                total += item.Product.Price * item.Quantity;
            }

            order.TotalPrice = total;

            var payment = await _paymentService.CreatePaymentAsync(order.Id, total)
                          ?? throw new Exception("Payment is null");

            if (payment.Id == Guid.Empty)
                throw new Exception("Invalid payment ID");

            order.IdPayment = payment.Id;

            _context.Orders.Add(order);
            _context.Payments.Add(payment);
            _context.ShoppingCarts.RemoveRange(cartItems);

            await _context.SaveChangesAsync();

            if (transaction != null)
                await transaction.CommitAsync();

            return new OrderResponse
            {
                OrderId = order.Id,
                OrderNumber = order.OrderNumber,
                Status = order.Status,
                Total = order.TotalPrice,
                CreatedAt = order.CreatedAt
            };
        }
        catch
        {
            if (transaction != null)
                await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task<Order?> GetOrderByIdAsync(Guid orderId)
    {
        return await _context.Orders
            .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
            .Include(o => o.Payment)
            .Include(o => o.DeliveryAddress)
            .FirstOrDefaultAsync(o => o.Id == orderId);
    }

    public async Task<List<Order>> GetOrdersByCustomerIdAsync(int customerId)
    {
        return await _context.Orders
            .Where(o => o.IdCustomer == customerId)
            .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
            .Include(o => o.Payment)
            .Include(o => o.DeliveryAddress)
            .OrderByDescending(o => o.CreatedAt)
            .ToListAsync();
    }

    public async Task<OrderDetailsDto?> GetOrderDetailsAsync(Guid orderId)
    {
        var order = await _context.Orders
            .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
            .Include(o => o.DeliveryAddress)
                .ThenInclude(da => da.City)
            .FirstOrDefaultAsync(o => o.Id == orderId);

        if (order == null) return null;

        return new OrderDetailsDto
        {
            OrderId = order.Id,
            OrderNumber = order.OrderNumber,
            Total = order.TotalPrice,
            Status = order.Status,
            CreatedAt = order.CreatedAt,
            DeliveryAddress = new DeliveryAddressDto
            {
                Id = order.DeliveryAddress.Id,
                StreetName = order.DeliveryAddress.StreetName,
                StreetNumber = order.DeliveryAddress.StreetNumber,
                StreetNameOptional = order.DeliveryAddress.StreetNameOptional,
                City = new CityDto
                {
                    Id = order.DeliveryAddress.City.Id,
                    Name = order.DeliveryAddress.City.Name,
                    PostalCode = order.DeliveryAddress.City.PostalCode
                }
            },
            Items = order.OrderItems.Select(oi => new OrderItemDto
            {
                ProductId = oi.IdProduct,
                ProductName = oi.Product.Name,
                Quantity = oi.Quantity,
                Price = oi.Price
            }).ToList()
        };
    }

    private async Task<IDbContextTransaction?> TryBeginTransactionAsync()
    {
        try
        {
            return await _context.Database.BeginTransactionAsync();
        }
        catch (InvalidOperationException ex)
        {
            if (ex.Message.Contains("Transactions are not supported"))
                return null;
            throw;
        }
    }

    private string GenerateOrderNumber()
    {
        return $"ORD-{DateTime.UtcNow:yyyyMMdd}-{Guid.NewGuid().ToString()[..6].ToUpper()}";
    }
}
