using API.Implementations.Domain.Customers;
using API.Services.Customers;
using Microsoft.Extensions.DependencyInjection;

namespace API.Utils.Extensions;

/// <summary>
/// Extension methods for configuring services.
/// </summary>
public static class ServiceExtensions
{
    /// <summary>
    /// Adds customer services to the service collection.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <returns>The service collection with customer services added.</returns>
    public static IServiceCollection AddCustomerServices(this IServiceCollection services)
    {
        // Register the customer services
        services.AddScoped<ICustomerService, CustomerService>();
        services.AddScoped<ICartService, CartService>();
        services.AddScoped<IPackageService, PackageService>();
        
        return services;
    }
}