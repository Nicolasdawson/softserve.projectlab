using API.Models.Customers;
using API.Services.Customers;

namespace API.Implementations.Domain.Customers;

/// <summary>
/// Implementation of the ICustomerService interface.
/// </summary>
public class CustomerService : ICustomerService
{
    // In a real application, this would be replaced with a database repository
    private readonly List<Customer> _customers = new();

    /// <summary>
    /// Gets all customers.
    /// </summary>
    /// <returns>A collection of all customers.</returns>
    public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
    {
        // Simulate async operation
        await Task.CompletedTask;
        return _customers;
    }

    /// <summary>
    /// Gets a customer by ID.
    /// </summary>
    /// <param name="id">The ID of the customer to retrieve.</param>
    /// <returns>The customer if found; otherwise, null.</returns>
    public async Task<Customer?> GetCustomerByIdAsync(string id)
    {
        await Task.CompletedTask;
        return _customers.FirstOrDefault(c => c.Id == id);
    }

    /// <summary>
    /// Gets customers by type.
    /// </summary>
    /// <param name="customerType">The type of customers to retrieve (Premium, Business, Individual).</param>
    /// <returns>A collection of customers of the specified type.</returns>
    public async Task<IEnumerable<Customer>> GetCustomersByTypeAsync(string customerType)
    {
        await Task.CompletedTask;

        return customerType.ToLower() switch
        {
            "premium" => _customers.Where(c => c is PremiumCustomer),
            "business" => _customers.Where(c => c is BusinessCustomer),
            "individual" => _customers.Where(c => c is IndividualCustomer),
            _ => Enumerable.Empty<Customer>()
        };
    }

    /// <summary>
    /// Creates a new customer.
    /// </summary>
    /// <param name="customer">The customer to create.</param>
    /// <returns>The created customer.</returns>
    public async Task<Customer> CreateCustomerAsync(Customer customer)
    {
        await Task.CompletedTask;
        
        // Ensure the customer has an ID
        if (string.IsNullOrEmpty(customer.Id))
        {
            customer.Id = Guid.NewGuid().ToString();
        }
        
        _customers.Add(customer);
        return customer;
    }

    /// <summary>
    /// Updates an existing customer.
    /// </summary>
    /// <param name="id">The ID of the customer to update.</param>
    /// <param name="customer">The updated customer information.</param>
    /// <returns>The updated customer if found; otherwise, null.</returns>
    public async Task<Customer?> UpdateCustomerAsync(string id, Customer customer)
    {
        await Task.CompletedTask;
        
        var existingCustomer = _customers.FirstOrDefault(c => c.Id == id);
        if (existingCustomer == null)
        {
            return null;
        }
        
        // Update properties
        // In a real application, you would use a mapping library or manually update each property
        // For simplicity, we'll just replace the customer
        customer.Id = id; // Ensure the ID remains the same
        _customers.Remove(existingCustomer);
        _customers.Add(customer);
        
        return customer;
    }

    /// <summary>
    /// Deletes a customer.
    /// </summary>
    /// <param name="id">The ID of the customer to delete.</param>
    /// <returns>True if the customer was deleted; otherwise, false.</returns>
    public async Task<bool> DeleteCustomerAsync(string id)
    {
        await Task.CompletedTask;
        
        var customer = _customers.FirstOrDefault(c => c.Id == id);
        if (customer == null)
        {
            return false;
        }
        
        return _customers.Remove(customer);
    }

    /// <summary>
    /// Creates a line of credit for a customer.
    /// </summary>
    /// <param name="customerId">The ID of the customer.</param>
    /// <param name="lineOfCredit">The line of credit to create.</param>
    /// <returns>The created line of credit.</returns>
    public async Task<LineOfCredit> CreateLineOfCreditAsync(string customerId, LineOfCredit lineOfCredit)
    {
        var customer = await GetCustomerByIdAsync(customerId);
        if (customer == null)
        {
            throw new KeyNotFoundException($"Customer with ID {customerId} not found.");
        }
        
        customer.LineOfCredit = lineOfCredit;
        return lineOfCredit;
    }

    /// <summary>
    /// Gets a customer's line of credit.
    /// </summary>
    /// <param name="customerId">The ID of the customer.</param>
    /// <returns>The customer's line of credit if found; otherwise, null.</returns>
    public async Task<LineOfCredit?> GetLineOfCreditAsync(string customerId)
    {
        var customer = await GetCustomerByIdAsync(customerId);
        return customer?.LineOfCredit;
    }

    /// <summary>
    /// Updates a customer's line of credit.
    /// </summary>
    /// <param name="customerId">The ID of the customer.</param>
    /// <param name="lineOfCredit">The updated line of credit information.</param>
    /// <returns>The updated line of credit if found; otherwise, null.</returns>
    public async Task<LineOfCredit?> UpdateLineOfCreditAsync(string customerId, LineOfCredit lineOfCredit)
    {
        var customer = await GetCustomerByIdAsync(customerId);
        if (customer == null || customer.LineOfCredit == null)
        {
            return null;
        }
        
        // Preserve the ID
        lineOfCredit.Id = customer.LineOfCredit.Id;
        
        // Update the line of credit
        customer.LineOfCredit = lineOfCredit;
        
        return lineOfCredit;
    }

    /// <summary>
    /// Searches for customers based on search criteria.
    /// </summary>
    /// <param name="searchTerm">The search term to match against customer properties.</param>
    /// <returns>A collection of customers matching the search criteria.</returns>
    public async Task<IEnumerable<Customer>> SearchCustomersAsync(string searchTerm)
    {
        await Task.CompletedTask;
        
        searchTerm = searchTerm.ToLower();
        
        return _customers.Where(c => 
            c.FirstName.ToLower().Contains(searchTerm) || 
            c.LastName.ToLower().Contains(searchTerm) || 
            c.Email.ToLower().Contains(searchTerm) || 
            c.PhoneNumber.ToLower().Contains(searchTerm) ||
            c.GetFullName().ToLower().Contains(searchTerm)
        );
    }

    /// <summary>
    /// Gets the transaction history for a customer's line of credit.
    /// </summary>
    /// <param name="customerId">The ID of the customer.</param>
    /// <returns>A collection of credit transactions if found; otherwise, null.</returns>
    public async Task<IEnumerable<CreditTransaction>?> GetCreditTransactionHistoryAsync(string customerId)
    {
        var lineOfCredit = await GetLineOfCreditAsync(customerId);
        return lineOfCredit?.GetTransactionHistory();
    }

    /// <summary>
    /// Makes a payment on a customer's line of credit.
    /// </summary>
    /// <param name="customerId">The ID of the customer.</param>
    /// <param name="amount">The payment amount.</param>
    /// <param name="description">Optional description of the payment.</param>
    /// <returns>The updated line of credit if found; otherwise, null.</returns>
    public async Task<LineOfCredit?> MakePaymentAsync(string customerId, decimal amount, string? description = null)
    {
        var lineOfCredit = await GetLineOfCreditAsync(customerId);
        if (lineOfCredit == null)
        {
            return null;
        }
        
        lineOfCredit.Deposit(amount, description ?? "Payment");
        return lineOfCredit;
    }

    /// <summary>
    /// Makes a charge on a customer's line of credit.
    /// </summary>
    /// <param name="customerId">The ID of the customer.</param>
    /// <param name="amount">The charge amount.</param>
    /// <param name="description">Optional description of the charge.</param>
    /// <returns>The updated line of credit if found; otherwise, null.</returns>
    public async Task<LineOfCredit?> MakeChargeAsync(string customerId, decimal amount, string? description = null)
    {
        var lineOfCredit = await GetLineOfCreditAsync(customerId);
        if (lineOfCredit == null)
        {
            return null;
        }
        
        lineOfCredit.Withdraw(amount, description ?? "Purchase");
        return lineOfCredit;
    }
}