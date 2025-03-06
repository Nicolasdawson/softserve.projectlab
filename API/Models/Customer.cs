namespace API.Models;

/// <summary>
/// Represents a customer with personal and financial information.
/// </summary>
public class Customer
{
    /// <summary>
    /// Gets or sets the first name of the customer.
    /// </summary>
    public string FirstName { get; set; }

    /// <summary>
    /// Gets or sets the last name of the customer.
    /// </summary>
    public string LastName { get; set; }

    /// <summary>
    /// Gets or sets the birth date of the customer.
    /// </summary>
    public DateOnly BirthDate { get; set; }

    /// <summary>
    /// Gets or sets the email address of the customer.
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Gets or sets the line of credit for the customer.
    /// </summary>
    public LineOfCredit? LineOfCredit { get; set; }
}

public class PremiumCustomer : Customer
{
    public decimal DiscountRate { get; set; }
    public DateTime MembershipStartDate { get; set; }
}
