using Xunit;
using Moq;
using Microsoft.EntityFrameworkCore;
using API.Services;
using API.Models;
using API.implementations.Infrastructure.Data;

public class OrderServiceTests
{
    [Fact]
    public async Task CreateOrderAsync_ShouldCreateOrder_WhenValidData()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("TestDB")
            .Options;

        await using var context = new AppDbContext(options);
        await context.Database.EnsureDeletedAsync();
        await context.Database.EnsureCreatedAsync();

        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = "Producto Test",
            Description = "Descripción",
            Price = 100,
            Stock = 10
        };

        var city = new City
        {
            Id = Guid.NewGuid(),
            Name = "Ciudad Test",
            PostalCode = "1234"
        };

        var customer = new Customer
        {
            Id = 1,
            Email = "test@test.com",
            FirstName = "Juan",
            LastName = "Pérez",
            PhoneNumber = "123456",
            IsGuest = false,
            StartDate = DateTime.UtcNow,
            IsCurrent = true
        };

        var address = new DeliveryAddress
        {
            Id = Guid.NewGuid(),
            StreetName = "Calle Falsa",
            StreetNumber = "123",
            StreetNameOptional = "",
            IdCity = city.Id,
            City = city
        };

        var shoppingCart = new ShoppingCart
        {
            Id = Guid.NewGuid(),
            IdCustomer = customer.Id,
            IdProduct = product.Id,
            Quantity = 2,
            Product = product
        };

        await context.Cities.AddAsync(city);
        await context.Products.AddAsync(product);
        await context.Customers.AddAsync(customer);
        await context.DeliveryAddresses.AddAsync(address);
        await context.ShoppingCarts.AddAsync(shoppingCart);
        await context.SaveChangesAsync();

        var stockReservationServiceMock = new Mock<IStockReservationService>();
        stockReservationServiceMock
            .Setup(s => s.TryReserveStockAsync(It.IsAny<Guid>(), It.IsAny<int>()))
            .ReturnsAsync(true);

        var paymentServiceMock = new Mock<IPaymentService>();
        paymentServiceMock
            .Setup(p => p.CreatePaymentAsync(It.IsAny<Guid>(), It.IsAny<decimal>()))
            .ReturnsAsync((Guid orderId, decimal amount) => new Payment
            {
                Id = Guid.NewGuid(),
                StripeSessionId = "session_123",
                Amount = amount,
                Currency = "usd",
                Status = "unpaid",
                CreatedAt = DateTime.UtcNow
            });

        var orderService = new OrderService(context, stockReservationServiceMock.Object, paymentServiceMock.Object);

        // Act
        var result = await orderService.CreateOrderFromCartAsync(customer.Id, address.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("created", result.Status);
        Assert.Equal(200, result.Total);
        Assert.NotEqual(Guid.Empty, result.OrderId);
        Assert.False(string.IsNullOrWhiteSpace(result.OrderNumber));

        var orderInDb = await context.Orders
            .Include(o => o.OrderItems)
            .FirstOrDefaultAsync(o => o.Id == result.OrderId);

        Assert.NotNull(orderInDb);
        Assert.Single(orderInDb.OrderItems);
        Assert.Equal(product.Id, orderInDb.OrderItems.First().IdProduct);
        Assert.Equal(2, orderInDb.OrderItems.First().Quantity);
    }
}
