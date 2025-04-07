using API.Implementations.Domain;
using API.Models;
using API.Models.Customers;
using API.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly CustomerDomain _customerDomain;

        public CustomerService(CustomerDomain customerDomain)
        {
            _customerDomain = customerDomain;
        }

        public async Task<Result<Customer>> AddCustomerAsync(Customer customer)
        {
            return await _customerDomain.CreateCustomerAsync(customer);
        }

        public async Task<Result<Customer>> GetCustomerByIdAsync(int customerId)
        {
            return await _customerDomain.GetCustomerByIdAsync(customerId);
        }

        public async Task<Result<List<Customer>>> GetAllCustomersAsync()
        {
            return await _customerDomain.GetAllCustomersAsync();
        }
    }
}