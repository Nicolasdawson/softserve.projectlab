using API.Models;
using API.Models.Customers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<Result<Customer>> AddCustomerAsync(Customer customer);
        Task<Result<Customer>> GetCustomerByIdAsync(int customerId);
        Task<Result<List<Customer>>> GetAllCustomersAsync();
    }
}