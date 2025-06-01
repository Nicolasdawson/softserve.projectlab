using Xunit;
using Microsoft.EntityFrameworkCore;
using API.Services;
using API.implementations.Infrastructure.Data;

namespace API.Tests.Services;

public class ShoppingCartServiceNegativeTests
{
    private async Task<AppDbContext> GetInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        var context = new AppDbContext(options);
        await context.Database.EnsureCreatedAsync();
        return context;
    }

    [Fact]
    public async Task UpdateQuantityAsync_ShouldThrowException_WhenItemDoesNotExist()
    {
        // Arrange
        var dbContext = await GetInMemoryDbContext();
        var service = new ShoppingCartService(dbContext);
        int customerId = 999;
        Guid productId = Guid.NewGuid();

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(async () =>
        {
            await service.UpdateQuantityAsync(customerId, productId, 5);
        });
    }

    [Fact]
    public async Task RemoveFromCartAsync_ShouldNotThrow_WhenItemDoesNotExist()
    {
        // Arrange
        var dbContext = await GetInMemoryDbContext();
        var service = new ShoppingCartService(dbContext);
        Guid fakeCartItemId = Guid.NewGuid();

        // Act (no debería lanzar excepción)
        var exception = await Record.ExceptionAsync(() => service.RemoveFromCartAsync(fakeCartItemId));

        // Assert
        Assert.Null(exception);
    }

    [Fact]
    public async Task ClearCartAsync_ShouldDoNothing_WhenCartIsAlreadyEmpty()
    {
        // Arrange
        var dbContext = await GetInMemoryDbContext();
        var service = new ShoppingCartService(dbContext);
        int customerId = 123;

        // Act
        await service.ClearCartAsync(customerId);

        // Assert
        var cart = await dbContext.ShoppingCarts
            .Where(c => c.IdCustomer == customerId)
            .ToListAsync();

        Assert.Empty(cart); // El carrito sigue vacío sin error
    }

    [Fact]
    public async Task GetCartByCustomerAsync_ShouldReturnEmptyList_WhenCartDoesNotExist()
    {
        // Arrange
        var dbContext = await GetInMemoryDbContext();
        var service = new ShoppingCartService(dbContext);
        int customerId = 555;

        // Act
        var cart = await service.GetCartByCustomerAsync(customerId);

        // Assert
        Assert.NotNull(cart);
        Assert.Empty(cart);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-5)]
    public async Task AddToCartAsync_ShouldThrow_WhenQuantityIsInvalid(int quantity)
    {
        var dbContext = await GetInMemoryDbContext();
        var service = new ShoppingCartService(dbContext);

        await Assert.ThrowsAsync<ArgumentException>(() =>
            service.AddToCartAsync(1, Guid.NewGuid(), quantity));
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(-10)]
    public async Task UpdateQuantityAsync_ShouldThrow_WhenQuantityIsNegative(int invalidQuantity)
    {
        var dbContext = await GetInMemoryDbContext();
        var service = new ShoppingCartService(dbContext);

        // El producto no existe en la base
        var ex = await Assert.ThrowsAsync<ArgumentException>(() =>
            service.UpdateQuantityAsync(1, Guid.NewGuid(), invalidQuantity));
            Assert.Equal("Quantity cannot be negative. (Parameter 'newQuantity')", ex.Message);
    }

}

