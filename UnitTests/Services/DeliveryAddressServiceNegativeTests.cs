using Xunit;
using API.Services;
using API.Models;
using API.implementations.Infrastructure.Data;
using API.DTO.DeliveryAddress;
using Microsoft.EntityFrameworkCore;

namespace API.Tests.Services;

public class DeliveryAddressServiceNegativeTests
{
    private AppDbContext GetDbContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        return new AppDbContext(options);
    }

    [Fact]
    public async Task CreateFromRequestAsync_ShouldReturnNull_WhenRegionDoesNotExist()
    {
        // Arrange
        var context = GetDbContext();
        var service = new DeliveryAddressService(context);

        var request = new DeliveryAddressRequest
        {
            StreetName = "Fake St",
            StreetNumber = "1",
            StreetNameOptional = null,
            CityName = "Ghost City",
            IdRegion = Guid.NewGuid() // Región no existente
        };

        // Act
        var result = await service.CreateFromRequestAsync(request);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task CreateFromRequestAsync_ShouldThrow_WhenRequestIsNull()
    {
        // Arrange
        var context = GetDbContext();
        var service = new DeliveryAddressService(context);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => service.CreateFromRequestAsync(null));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public async Task CreateFromRequestAsync_ShouldThrow_WhenCityNameIsNullOrEmpty(string? cityName)
    {
        // Arrange
        var context = GetDbContext();

        // Crear región válida
        var region = new Region { Id = Guid.NewGuid(), Name = "Test Region" };
        context.Regions.Add(region);
        await context.SaveChangesAsync();

        var service = new DeliveryAddressService(context);

        var request = new DeliveryAddressRequest
        {
            StreetName = "Fake St",
            StreetNumber = "1",
            StreetNameOptional = null,
            CityName = cityName!,
            IdRegion = region.Id
        };

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => service.CreateFromRequestAsync(request));
    }


}
