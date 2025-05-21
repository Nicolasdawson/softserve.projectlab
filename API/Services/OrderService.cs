using API.implementations.Infrastructure.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;
using Stripe;
using API.DTO.Order;

namespace API.Services;

public class OrderService
{
    private readonly AppDbContext _context;

    public OrderService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<OrderResponse> CreateOrderFromCartAsync(int customerId, Guid deliveryAddressId)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();

        var customer = await _context.Customers.FindAsync(customerId)
                    ?? throw new Exception("Customer not found");

        var cartItems = await _context.ShoppingCarts
            .Where(c => c.IdCustomer == customerId)
            .Include(c => c.Product)
            .ToListAsync();

        if (cartItems == null || !cartItems.Any())
            throw new Exception("Shopping cart is empty");

        foreach (var item in cartItems)
        {
            if (item.Product.Stock < item.Quantity)
                throw new Exception($"Insufficient stock for product {item.Product.Name}");
        }

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
            item.Product.Stock -= item.Quantity;

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

        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        var payment = await CreateStripePaymentIntent(order.Id, total);

        _context.Payments.Add(payment);
        order.IdPayment = payment.Id;

        _context.ShoppingCarts.RemoveRange(cartItems);

        await _context.SaveChangesAsync();
        await transaction.CommitAsync();

        // Retorná sólo datos planos, no entidades completas
        return new OrderResponse
        {
            OrderId = order.Id,
            OrderNumber = order.OrderNumber,
            Status = order.Status,
            Total = order.TotalPrice,
            CreatedAt = order.CreatedAt
        };
    }


    private async Task<Payment> CreateStripePaymentIntent(Guid orderId, decimal total)
    {
        var stripeService = new PaymentIntentService();
        var intent = await stripeService.CreateAsync(new PaymentIntentCreateOptions
        {
            Amount = (long)(total * 100),
            Currency = "usd",
            Metadata = new Dictionary<string, string> { { "order_id", orderId.ToString() } }
        });

        return new Payment
        {
            Id = Guid.NewGuid(),
            StripeSessionId = intent.Id,
            PaymentIntentId = intent.Id,
            Amount = total,
            Currency = "usd",
            Status = "unpaid",
            CreatedAt = DateTime.UtcNow,
            IdOrder = orderId
        };
    }

    private string GenerateOrderNumber()
    {
        return $"ORD-{DateTime.UtcNow:yyyyMMdd}-{Guid.NewGuid().ToString().Substring(0, 6).ToUpper()}";
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


}
