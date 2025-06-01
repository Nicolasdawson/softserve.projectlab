using Xunit;
using Microsoft.EntityFrameworkCore;
using API.Models;
using API.implementations.Infrastructure.Data;
using API.DTO.DeliveryAddress;
using API.Services;

namespace API.Tests.Services;

public class DeliveryAddressServiceTests
{
    private async Task<AppDbContext> GetInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        var context = new AppDbContext(options);

        // Datos precargados
        var country = new Country
        {
            Id = Guid.Parse("A1111111-C1D4-478B-9C30-0000C409754B"),
            Name = "Testland"
        };

        var region = new Region
        {
            Id = Guid.Parse("7285AD1B-C1D4-478B-9C30-0000C409754B"),
            Name = "Test Region",
            IdCountry = country.Id
        };

        var city = new City
        {
            Id = Guid.Parse("B2222222-C1D4-478B-9C30-0000C409754B"),
            Name = "Test City",
            PostalCode = "12345",
            IdRegion = region.Id
        };

        await context.Countries.AddAsync(country);
        await context.Regions.AddAsync(region);
        await context.Cities.AddAsync(city);
        await context.SaveChangesAsync();

        return context;
    }

    [Fact]
    public async Task CreateFromRequestAsync_ShouldCreateDeliveryAddress_WhenValidDataProvided()
    {
        // Arrange
        var dbContext = await GetInMemoryDbContext();
        var service = new DeliveryAddressService(dbContext);

        var request = new DeliveryAddressRequest
        {
            StreetName = "Main Street",
            StreetNumber = "123",
            StreetNameOptional = "Apt 4B",
            CityName = "Test City", 
            IdRegion = Guid.Parse("7285AD1B-C1D4-478B-9C30-0000C409754B")
        };

        // Act
        var result = await service.CreateFromRequestAsync(request);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(request.StreetName, result.StreetName);
        Assert.Equal(request.StreetNumber, result.StreetNumber);
        Assert.Equal(request.StreetNameOptional, result.StreetNameOptional);
        Assert.Equal("Test City", result.City.Name);
    }
}
