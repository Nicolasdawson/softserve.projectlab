using API.Models.Customers;

namespace API.Services.Customers;

/// <summary>
/// Provides operations for managing lines of credit.
/// </summary>
public interface ILineOfCreditService
{
    /// <summary>
    /// Gets a line of credit by ID.
    /// </summary>
    /// <param name="id">The ID of the line of credit to retrieve.</param>
    /// <returns>The line of credit if found; otherwise, null.</returns>
    Task<LineOfCredit?> GetLineOfCreditByIdAsync(string id);

    /// <summary>
    /// Creates a new line of credit for a customer.
    /// </summary>
    /// <param name="customerId">The ID of the customer.</param>
    /// <param name="lineOfCredit">The line of credit to create.</param>
    /// <returns>The created line of credit.</returns>
    Task<LineOfCredit> CreateLineOfCreditAsync(string customerId, LineOfCredit lineOfCredit);

    /// <summary>
    /// Updates an existing line of credit.
    /// </summary>
    /// <param name="id">The ID of the line of credit to update.</param>
    /// <param name="lineOfCredit">The updated line of credit information.</param>
    /// <returns>The updated line of credit if found; otherwise, null.</returns>
    Task<LineOfCredit?> UpdateLineOfCreditAsync(string id, LineOfCredit lineOfCredit);

    /// <summary>
    /// Makes a payment on a line of credit.
    /// </summary>
    /// <param name="id">The ID of the line of credit.</param>
    /// <param name="amount">The payment amount.</param>
    /// <param name="description">Optional description of the payment.</param>
    /// <returns>The updated line of credit if found; otherwise, null.</returns>
    Task<LineOfCredit?> MakePaymentAsync(string id, decimal amount, string? description = null);

    /// <summary>
    /// Makes a charge on a line of credit.
    /// </summary>
    /// <param name="id">The ID of the line of credit.</param>
    /// <param name="amount">The charge amount.</param>
    /// <param name="description">Optional description of the charge.</param>
    /// <returns>The updated line of credit if found; otherwise, null.</returns>
    Task<LineOfCredit?> MakeChargeAsync(string id, decimal amount, string? description = null);

    /// <summary>
    /// Gets the transaction history for a line of credit.
    /// </summary>
    /// <param name="id">The ID of the line of credit.</param>
    /// <returns>A collection of credit transactions if found; otherwise, null.</returns>
    Task<IEnumerable<CreditTransaction>?> GetTransactionHistoryAsync(string id);

    /// <summary>
    /// Applies interest to a line of credit.
    /// </summary>
    /// <param name="id">The ID of the line of credit.</param>
    /// <returns>The updated line of credit if found; otherwise, null.</returns>
    Task<LineOfCredit?> ApplyInterestAsync(string id);

    /// <summary>
    /// Increases the credit limit for a line of credit.
    /// </summary>
    /// <param name="id">The ID of the line of credit.</param>
    /// <param name="newLimit">The new credit limit.</param>
    /// <param name="approvedBy">The user who approved the increase.</param>
    /// <returns>The updated line of credit if found; otherwise, null.</returns>
    Task<LineOfCredit?> IncreaseCreditLimitAsync(string id, decimal newLimit, string approvedBy);

    /// <summary>
    /// Gets all past due lines of credit.
    /// </summary>
    /// <returns>A collection of past due lines of credit.</returns>
    Task<IEnumerable<LineOfCredit>> GetPastDueLinesOfCreditAsync();
}