using API.Models.Customers;

namespace API.Services.Customers;

/// <summary>
/// Provides operations specific to business customers.
/// </summary>
public interface IBusinessCustomerService
{
    /// <summary>
    /// Gets all business customers.
    /// </summary>
    /// <returns>A collection of all business customers.</returns>
    Task<IEnumerable<BusinessCustomer>> GetAllBusinessCustomersAsync();

    /// <summary>
    /// Gets a business customer by ID.
    /// </summary>
    /// <param name="id">The ID of the business customer to retrieve.</param>
    /// <returns>The business customer if found; otherwise, null.</returns>
    Task<BusinessCustomer?> GetBusinessCustomerByIdAsync(string id);

    /// <summary>
    /// Creates a new business customer.
    /// </summary>
    /// <param name="customer">The business customer to create.</param>
    /// <returns>The created business customer.</returns>
    Task<BusinessCustomer> CreateBusinessCustomerAsync(BusinessCustomer customer);

    /// <summary>
    /// Updates an existing business customer.
    /// </summary>
    /// <param name="id">The ID of the business customer to update.</param>
    /// <param name="customer">The updated business customer information.</param>
    /// <returns>The updated business customer if found; otherwise, null.</returns>
    Task<BusinessCustomer?> UpdateBusinessCustomerAsync(string id, BusinessCustomer customer);

    /// <summary>
    /// Deletes a business customer.
    /// </summary>
    /// <param name="id">The ID of the business customer to delete.</param>
    /// <returns>True if the business customer was deleted; otherwise, false.</returns>
    Task<bool> DeleteBusinessCustomerAsync(string id);

    /// <summary>
    /// Searches for business customers by industry.
    /// </summary>
    /// <param name="industry">The industry to search for.</param>
    /// <returns>A collection of business customers in the specified industry.</returns>
    Task<IEnumerable<BusinessCustomer>> GetBusinessCustomersByIndustryAsync(string industry);

    /// <summary>
    /// Gets business customers by size category.
    /// </summary>
    /// <param name="businessSize">The business size category (Small, Medium, Large, Enterprise).</param>
    /// <returns>A collection of business customers of the specified size.</returns>
    Task<IEnumerable<BusinessCustomer>> GetBusinessCustomersBySizeAsync(string businessSize);

    /// <summary>
    /// Calculates the volume discount for a business customer.
    /// </summary>
    /// <param name="customerId">The ID of the business customer.</param>
    /// <param name="orderValue">The order value to calculate the discount for.</param>
    /// <returns>The calculated discount amount if the customer is found; otherwise, 0.</returns>
    Task<decimal> CalculateVolumeDiscountAsync(string customerId, decimal orderValue);
}