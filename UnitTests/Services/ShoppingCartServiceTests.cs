using Xunit;
using Microsoft.EntityFrameworkCore;
using API.Models;
using API.Services;
using API.implementations.Infrastructure.Data;

namespace API.Tests.Services;
    public class ShoppingCartServiceTests
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
        public async Task AddToCartAsync_ShouldAddNewItem_WhenNotExists()
        {
            // Arrange
            var dbContext = await GetInMemoryDbContext();
            var service = new ShoppingCartService(dbContext);

            int customerId = 1;
            Guid productId = Guid.NewGuid();
            int quantity = 2;

            // Act
            var result = await service.AddToCartAsync(customerId, productId, quantity);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(customerId, result.IdCustomer);
            Assert.Equal(productId, result.IdProduct);
            Assert.Equal(quantity, result.Quantity);
        }

        [Fact]
        public async Task AddToCartAsync_ShouldUpdateQuantity_WhenItemExists()
        {
            // Arrange
            var dbContext = await GetInMemoryDbContext();
            var service = new ShoppingCartService(dbContext);

            int customerId = 1;
            Guid productId = Guid.NewGuid();

            // Simula un ítem ya existente
            dbContext.ShoppingCarts.Add(new ShoppingCart
            {
                Id = Guid.NewGuid(),
                IdCustomer = customerId,
                IdProduct = productId,
                Quantity = 1,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            });
            await dbContext.SaveChangesAsync();

            // Act
            var result = await service.AddToCartAsync(customerId, productId, 3); // debería sumar 1 + 3

            // Assert
            Assert.Equal(4, result.Quantity);
        }
    }
