using API.Models.Customers;

namespace API.Services.Customers;

/// <summary>
/// Provides operations specific to premium customers.
/// </summary>
public interface IPremiumCustomerService
{
    /// <summary>
    /// Gets all premium customers.
    /// </summary>
    /// <returns>A collection of all premium customers.</returns>
    Task<IEnumerable<PremiumCustomer>> GetAllPremiumCustomersAsync();

    /// <summary>
    /// Gets a premium customer by ID.
    /// </summary>
    /// <param name="id">The ID of the premium customer to retrieve.</param>
    /// <returns>The premium customer if found; otherwise, null.</returns>
    Task<PremiumCustomer?> GetPremiumCustomerByIdAsync(string id);

    /// <summary>
    /// Creates a new premium customer.
    /// </summary>
    /// <param name="customer">The premium customer to create.</param>
    /// <returns>The created premium customer.</returns>
    Task<PremiumCustomer> CreatePremiumCustomerAsync(PremiumCustomer customer);

    /// <summary>
    /// Updates an existing premium customer.
    /// </summary>
    /// <param name="id">The ID of the premium customer to update.</param>
    /// <param name="customer">The updated premium customer information.</param>
    /// <returns>The updated premium customer if found; otherwise, null.</returns>
    Task<PremiumCustomer?> UpdatePremiumCustomerAsync(string id, PremiumCustomer customer);

    /// <summary>
    /// Deletes a premium customer.
    /// </summary>
    /// <param name="id">The ID of the premium customer to delete.</param>
    /// <returns>True if the premium customer was deleted; otherwise, false.</returns>
    Task<bool> DeletePremiumCustomerAsync(string id);

    /// <summary>
    /// Calculates the discounted price for a premium customer.
    /// </summary>
    /// <param name="customerId">The ID of the premium customer.</param>
    /// <param name="originalPrice">The original price before discount.</param>
    /// <returns>The discounted price if the customer is found; otherwise, the original price.</returns>
    Task<decimal> CalculateDiscountedPriceAsync(string customerId, decimal originalPrice);

    /// <summary>
    /// Extends a premium customer's membership by a specified number of months.
    /// </summary>
    /// <param name="customerId">The ID of the premium customer.</param>
    /// <param name="months">The number of months to extend the membership.</param>
    /// <returns>The updated premium customer with the extended membership expiry date.</returns>
    Task<PremiumCustomer?> ExtendMembershipAsync(string customerId, int months);

    /// <summary>
    /// Upgrades a premium customer's tier level.
    /// </summary>
    /// <param name="customerId">The ID of the premium customer.</param>
    /// <param name="newTierLevel">The new tier level (Silver, Gold, Platinum).</param>
    /// <returns>The updated premium customer with the new tier level.</returns>
    Task<PremiumCustomer?> UpgradeTierLevelAsync(string customerId, string newTierLevel);

    /// <summary>
    /// Gets premium customers whose memberships are expiring within a specified number of days.
    /// </summary>
    /// <param name="days">The number of days from the current date.</param>
    /// <returns>A collection of premium customers whose memberships are expiring within the specified days.</returns>
    Task<IEnumerable<PremiumCustomer>> GetExpiringMembershipsAsync(int days);
}