using Xunit;
using Moq;
using API.Services;
using API.Models;
using API.Abstractions;
using API.implementations.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public class OrderServiceNegativeTests
{
    private AppDbContext GetDbContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        return new AppDbContext(options);
    }

    [Fact]
    public async Task CreateOrderAsync_ShouldThrow_WhenCustomerNotFound()
    {
        // Arrange
        await using var context = GetDbContext();

        var stockServiceMock = new Mock<IStockReservationService>();
        var paymentServiceMock = new Mock<IPaymentService>();

        var service = new OrderService(context, stockServiceMock.Object, paymentServiceMock.Object);

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() =>
            service.CreateOrderFromCartAsync(999, Guid.NewGuid()));
    }

    [Fact]
    public async Task CreateOrderAsync_ShouldThrow_WhenCartIsEmpty()
    {
        // Arrange
        await using var context = GetDbContext();
        var customer = new Customer
        {
            Id = 1,
            Email = "fake@example.com",
            FirstName = "Test",
            LastName = "User",
            PhoneNumber = "1234567890"
        };

        await context.Customers.AddAsync(customer);
        await context.SaveChangesAsync();

        var stockServiceMock = new Mock<IStockReservationService>();
        var paymentServiceMock = new Mock<IPaymentService>();

        var service = new OrderService(context, stockServiceMock.Object, paymentServiceMock.Object);

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() =>
            service.CreateOrderFromCartAsync(1, Guid.NewGuid()));
    }

    [Fact]
    public async Task CreateOrderAsync_ShouldThrow_WhenStockIsInsufficient()
    {
        // Arrange
        await using var context = GetDbContext();

        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = "Test Product",
            Price = 100,
            Description = "Fake description"
        };

        var customer = new Customer
        {
            Id = 1,
            Email = "test@example.com",
            FirstName = "Test",
            LastName = "User",
            PhoneNumber = "1234567890"
        };

        var cart = new ShoppingCart
        {
            Id = Guid.NewGuid(),
            IdCustomer = customer.Id,
            IdProduct = product.Id,
            Quantity = 2,
            Product = product
        };

        await context.Products.AddAsync(product);
        await context.Customers.AddAsync(customer);
        await context.ShoppingCarts.AddAsync(cart);
        await context.SaveChangesAsync();

        var stockServiceMock = new Mock<IStockReservationService>();
        stockServiceMock.Setup(x => x.TryReserveStockAsync(product.Id, It.IsAny<int>()))
            .ReturnsAsync(false); // Stock insuficiente

        var paymentServiceMock = new Mock<IPaymentService>();

        var service = new OrderService(context, stockServiceMock.Object, paymentServiceMock.Object);

        // Act & Assert
        var ex = await Assert.ThrowsAsync<Exception>(() =>
            service.CreateOrderFromCartAsync(customer.Id, Guid.NewGuid()));

        Assert.Contains("Insufficient stock", ex.Message);
    }

    [Fact]
    public async Task CreateOrderAsync_ShouldThrow_WhenPaymentIsNull()
    {
        // Arrange
        await using var context = GetDbContext();

        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = "Test Product",
            Price = 100,
            Description = "Fake description"
        };

        var customer = new Customer
        {
            Id = 1,
            Email = "test@example.com",
            FirstName = "Test",
            LastName = "User",
            PhoneNumber = "1234567890"
        };

        var cart = new ShoppingCart
        {
            Id = Guid.NewGuid(),
            IdCustomer = customer.Id,
            IdProduct = product.Id,
            Quantity = 1,
            Product = product
        };

        await context.Products.AddAsync(product);
        await context.Customers.AddAsync(customer);
        await context.ShoppingCarts.AddAsync(cart);
        await context.SaveChangesAsync();

        var stockServiceMock = new Mock<IStockReservationService>();
        stockServiceMock.Setup(x => x.TryReserveStockAsync(product.Id, 1))
            .ReturnsAsync(true);

        var paymentServiceMock = new Mock<IPaymentService>();
        paymentServiceMock.Setup(x => x.CreatePaymentAsync(It.IsAny<Guid>(), It.IsAny<decimal>()))
            .ReturnsAsync((Payment?)null); // Simula fallo en pago

        var service = new OrderService(context, stockServiceMock.Object, paymentServiceMock.Object);

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() =>
            service.CreateOrderFromCartAsync(customer.Id, Guid.NewGuid()));
    }

    [Fact]
    public async Task CreateOrderAsync_ShouldThrow_WhenPaymentIdIsEmpty()
    {
        // Arrange
        await using var context = GetDbContext();

        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = "Test Product",
            Price = 100,
            Description = "Fake description"
        };

        var customer = new Customer
        {
            Id = 1,
            Email = "test@example.com",
            FirstName = "Test",
            LastName = "User",
            PhoneNumber = "1234567890"
        };
        var cart = new ShoppingCart
        {
            Id = Guid.NewGuid(),
            IdCustomer = customer.Id,
            IdProduct = product.Id,
            Quantity = 1,
            Product = product
        };

        await context.Products.AddAsync(product);
        await context.Customers.AddAsync(customer);
        await context.ShoppingCarts.AddAsync(cart);
        await context.SaveChangesAsync();

        var stockServiceMock = new Mock<IStockReservationService>();
        stockServiceMock.Setup(x => x.TryReserveStockAsync(product.Id, 1))
            .ReturnsAsync(true);

        var paymentServiceMock = new Mock<IPaymentService>();
        paymentServiceMock.Setup(x => x.CreatePaymentAsync(It.IsAny<Guid>(), It.IsAny<decimal>()))
            .ReturnsAsync(new Payment
            {
                Id = Guid.Empty, // ID inválido
                Amount = 100,
                Currency = "usd",
                Status = "unpaid"
            });

        var service = new OrderService(context, stockServiceMock.Object, paymentServiceMock.Object);

        // Act & Assert
        var ex = await Assert.ThrowsAsync<Exception>(() =>
            service.CreateOrderFromCartAsync(customer.Id, Guid.NewGuid()));

        Assert.Contains("payment", ex.Message.ToLower());
    }
}
