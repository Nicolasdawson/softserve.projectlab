using API.Models;

namespace API.Services;

public interface ICustomerService
{
    Task<Customer> CreateCustomerAsync(Customer customer);
}
