using API.implementations.Infrastructure.Data;
using API.Models;

namespace API.Services;

public class CustomerService : ICustomerService
{
    private readonly AppDbContext _context;
    public CustomerService(AppDbContext context) 
    {
        _context = context;
    }

    /// <summary>
    /// Creates a new Customer.
    /// </summary>
    /// <param name="customer">The customer to create.</param>
    /// <returns>The created customer.</returns>
    public async Task<Customer> CreateCustomerAsync(Customer customer)
    {
        _context.Customers.Add(customer);
        await _context.SaveChangesAsync();        
        
        return await Task.FromResult(customer);
    }
}
