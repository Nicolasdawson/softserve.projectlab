using API.Models.Customers;

namespace API.Services.Customers;

/// <summary>
/// Provides operations for managing customers.
/// </summary>
public interface ICustomerService
{
    /// <summary>
    /// Gets all customers.
    /// </summary>
    /// <returns>A collection of all customers.</returns>
    Task<IEnumerable<Customer>> GetAllCustomersAsync();

    /// <summary>
    /// Gets a customer by ID.
    /// </summary>
    /// <param name="id">The ID of the customer to retrieve.</param>
    /// <returns>The customer if found; otherwise, null.</returns>
    Task<Customer?> GetCustomerByIdAsync(string id);

    /// <summary>
    /// Gets customers by type.
    /// </summary>
    /// <param name="customerType">The type of customers to retrieve (Premium, Business, Individual).</param>
    /// <returns>A collection of customers of the specified type.</returns>
    Task<IEnumerable<Customer>> GetCustomersByTypeAsync(string customerType);

    /// <summary>
    /// Creates a new customer.
    /// </summary>
    /// <param name="customer">The customer to create.</param>
    /// <returns>The created customer.</returns>
    Task<Customer> CreateCustomerAsync(Customer customer);

    /// <summary>
    /// Updates an existing customer.
    /// </summary>
    /// <param name="id">The ID of the customer to update.</param>
    /// <param name="customer">The updated customer information.</param>
    /// <returns>The updated customer if found; otherwise, null.</returns>
    Task<Customer?> UpdateCustomerAsync(string id, Customer customer);

    /// <summary>
    /// Deletes a customer.
    /// </summary>
    /// <param name="id">The ID of the customer to delete.</param>
    /// <returns>True if the customer was deleted; otherwise, false.</returns>
    Task<bool> DeleteCustomerAsync(string id);

    /// <summary>
    /// Creates a line of credit for a customer.
    /// </summary>
    /// <param name="customerId">The ID of the customer.</param>
    /// <param name="lineOfCredit">The line of credit to create.</param>
    /// <returns>The created line of credit.</returns>
    Task<LineOfCredit> CreateLineOfCreditAsync(string customerId, LineOfCredit lineOfCredit);

    /// <summary>
    /// Gets a customer's line of credit.
    /// </summary>
    /// <param name="customerId">The ID of the customer.</param>
    /// <returns>The customer's line of credit if found; otherwise, null.</returns>
    Task<LineOfCredit?> GetLineOfCreditAsync(string customerId);

    /// <summary>
    /// Updates a customer's line of credit.
    /// </summary>
    /// <param name="customerId">The ID of the customer.</param>
    /// <param name="lineOfCredit">The updated line of credit information.</param>
    /// <returns>The updated line of credit if found; otherwise, null.</returns>
    Task<LineOfCredit?> UpdateLineOfCreditAsync(string customerId, LineOfCredit lineOfCredit);

    /// <summary>
    /// Searches for customers based on search criteria.
    /// </summary>
    /// <param name="searchTerm">The search term to match against customer properties.</param>
    /// <returns>A collection of customers matching the search criteria.</returns>
    Task<IEnumerable<Customer>> SearchCustomersAsync(string searchTerm);

    /// <summary>
    /// Gets the transaction history for a customer's line of credit.
    /// </summary>
    /// <param name="customerId">The ID of the customer.</param>
    /// <returns>A collection of credit transactions if found; otherwise, null.</returns>
    Task<IEnumerable<CreditTransaction>?> GetCreditTransactionHistoryAsync(string customerId);

    /// <summary>
    /// Makes a payment on a customer's line of credit.
    /// </summary>
    /// <param name="customerId">The ID of the customer.</param>
    /// <param name="amount">The payment amount.</param>
    /// <param name="description">Optional description of the payment.</param>
    /// <returns>The updated line of credit if found; otherwise, null.</returns>
    Task<LineOfCredit?> MakePaymentAsync(string customerId, decimal amount, string? description = null);

    /// <summary>
    /// Makes a charge on a customer's line of credit.
    /// </summary>
    /// <param name="customerId">The ID of the customer.</param>
    /// <param name="amount">The charge amount.</param>
    /// <param name="description">Optional description of the charge.</param>
    /// <returns>The updated line of credit if found; otherwise, null.</returns>
    Task<LineOfCredit?> MakeChargeAsync(string customerId, decimal amount, string? description = null);
}