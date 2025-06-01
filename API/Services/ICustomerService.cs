using API.Models;

namespace API.Services;

public interface ICustomerService
{
    Task<Customer> CreateCustomerAsync(Customer customer);

    Task<Customer?> GetByEmailAsync(string email);

    Task<Customer?> GetByIdAsync(int id);
}
