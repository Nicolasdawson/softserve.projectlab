using API.Models.Customers;

namespace API.Services.Customers;

/// <summary>
/// Provides operations specific to individual customers.
/// </summary>
public interface IIndividualCustomerService
{
    /// <summary>
    /// Gets all individual customers.
    /// </summary>
    /// <returns>A collection of all individual customers.</returns>
    Task<IEnumerable<IndividualCustomer>> GetAllIndividualCustomersAsync();

    /// <summary>
    /// Gets an individual customer by ID.
    /// </summary>
    /// <param name="id">The ID of the individual customer to retrieve.</param>
    /// <returns>The individual customer if found; otherwise, null.</returns>
    Task<IndividualCustomer?> GetIndividualCustomerByIdAsync(string id);

    /// <summary>
    /// Creates a new individual customer.
    /// </summary>
    /// <param name="customer">The individual customer to create.</param>
    /// <returns>The created individual customer.</returns>
    Task<IndividualCustomer> CreateIndividualCustomerAsync(IndividualCustomer customer);

    /// <summary>
    /// Updates an existing individual customer.
    /// </summary>
    /// <param name="id">The ID of the individual customer to update.</param>
    /// <param name="customer">The updated individual customer information.</param>
    /// <returns>The updated individual customer if found; otherwise, null.</returns>
    Task<IndividualCustomer?> UpdateIndividualCustomerAsync(string id, IndividualCustomer customer);

    /// <summary>
    /// Deletes an individual customer.
    /// </summary>
    /// <param name="id">The ID of the individual customer to delete.</param>
    /// <returns>True if the individual customer was deleted; otherwise, false.</returns>
    Task<bool> DeleteIndividualCustomerAsync(string id);

    /// <summary>
    /// Adds loyalty points to a customer's account based on a purchase.
    /// </summary>
    /// <param name="customerId">The ID of the customer.</param>
    /// <param name="purchaseAmount">The amount of the purchase.</param>
    /// <returns>The number of points added if the customer is found; otherwise, 0.</returns>
    Task<int> AddLoyaltyPointsAsync(string customerId, decimal purchaseAmount);

    /// <summary>
    /// Redeems loyalty points for a discount.
    /// </summary>
    /// <param name="customerId">The ID of the customer.</param>
    /// <param name="pointsToRedeem">The number of points to redeem.</param>
    /// <returns>The discount amount if the customer is found and has sufficient points; otherwise, 0.</returns>
    Task<decimal> RedeemLoyaltyPointsAsync(string customerId, int pointsToRedeem);

    /// <summary>
    /// Gets individual customers who are eligible for promotions.
    /// </summary>
    /// <returns>A collection of individual customers who are eligible for promotions.</returns>
    Task<IEnumerable<IndividualCustomer>> GetPromotionEligibleCustomersAsync();

    /// <summary>
    /// Gets individual customers by communication preference.
    /// </summary>
    /// <param name="preference">The communication preference (Email, SMS, Call).</param>
    /// <returns>A collection of individual customers with the specified communication preference.</returns>
    Task<IEnumerable<IndividualCustomer>> GetCustomersByCommunicationPreferenceAsync(string preference);
}