namespace API.Models.Customers;

/// <summary>
/// Represents a customer with personal and financial information.
/// </summary>
public class Customer
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    
    public DateOnly BirthDate { get; set; }
    
    public string Email { get; set; } = string.Empty;
    
    public string PhoneNumber { get; set; } = string.Empty;
    
    public string Address { get; set; } = string.Empty;
    
    public string City { get; set; } = string.Empty;
    
    public string State { get; set; } = string.Empty;
    
    public string ZipCode { get; set; } = string.Empty;
    
    public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;
    
    public LineOfCredit? LineOfCredit { get; set; }

    /// <summary>
    /// Gets the full name of the customer.
    /// </summary>
    /// <returns>The full name in the format "FirstName LastName".</returns>
    public string GetFullName()
    {
        return $"{FirstName} {LastName}";
    }
}

/// <summary>
/// Represents a premium customer with additional benefits.
/// </summary>
public class PremiumCustomer : Customer
{
    /// <summary>
    /// Gets or sets the discount rate applied to premium customers.
    /// </summary>
    public decimal DiscountRate { get; set; }

    /// <summary>
    /// Gets or sets the date when the premium membership started.
    /// </summary>
    public DateTime MembershipStartDate { get; set; }

    /// <summary>
    /// Gets or sets the date when the premium membership expires.
    /// </summary>
    public DateTime MembershipExpiryDate { get; set; }

    /// <summary>
    /// Gets or sets the premium tier level (e.g., Silver, Gold, Platinum).
    /// </summary>
    public string TierLevel { get; set; } = "Silver";

    /// <summary>
    /// Calculates the discounted price for a given amount based on the premium discount rate.
    /// </summary>
    /// <param name="originalPrice">The original price before discount.</param>
    /// <returns>The discounted price.</returns>
    public decimal CalculateDiscountedPrice(decimal originalPrice)
    {
        return originalPrice * (1 - DiscountRate / 100);
    }

    /// <summary>
    /// Checks if the premium membership is active.
    /// </summary>
    /// <returns>True if the membership is active, false otherwise.</returns>
    public bool IsMembershipActive()
    {
        return DateTime.UtcNow <= MembershipExpiryDate;
    }
}

/// <summary>
/// Represents a business customer with specialized business-related properties.
/// </summary>
public class BusinessCustomer : Customer
{
    public string CompanyName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the tax identification number.
    /// </summary>
    public string TaxId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the industry sector of the business.
    /// </summary>
    public string Industry { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the number of employees in the company.
    /// </summary>
    public int EmployeeCount { get; set; }

    /// <summary>
    /// Gets or sets the annual revenue of the business.
    /// </summary>
    public decimal AnnualRevenue { get; set; }

    /// <summary>
    /// Gets or sets the business size category (Small, Medium, Large, Enterprise).
    /// </summary>
    public string BusinessSize { get; set; } = "Small";

    /// <summary>
    /// Gets or sets the volume discount rate based on order size.
    /// </summary>
    public decimal VolumeDiscountRate { get; set; }

    /// <summary>
    /// Gets or sets the credit terms for the business (e.g., Net30, Net60).
    /// </summary>
    public string CreditTerms { get; set; } = "Net30";

    /// <summary>
    /// Calculates the volume discount for a given amount based on the total order value.
    /// </summary>
    /// <param name="orderValue">The total value of the order.</param>
    /// <returns>The discount amount.</returns>
    public decimal CalculateVolumeDiscount(decimal orderValue)
    {
        // Apply graduated volume discounts based on order size
        if (orderValue >= 10000)
        {
            return orderValue * (VolumeDiscountRate + 5) / 100;
        }
        else if (orderValue >= 5000)
        {
            return orderValue * (VolumeDiscountRate + 2) / 100;
        }
        else if (orderValue >= 1000)
        {
            return orderValue * VolumeDiscountRate / 100;
        }
        
        return 0;
    }
}

/// <summary>
/// Represents an individual customer with basic consumer-related properties.
/// </summary>
public class IndividualCustomer : Customer
{
    /// <summary>
    /// Gets or sets whether the customer is eligible for promotions.
    /// </summary>
    public bool IsEligibleForPromotions { get; set; } = true;

    /// <summary>
    /// Gets or sets the customer's preference for communication (Email, SMS, Call).
    /// </summary>
    public string CommunicationPreference { get; set; } = "Email";

    /// <summary>
    /// Gets or sets the loyalty points accumulated by the customer.
    /// </summary>
    public int LoyaltyPoints { get; set; }

    /// <summary>
    /// Gets or sets the date of the last purchase.
    /// </summary>
    public DateTime? LastPurchaseDate { get; set; }

    /// <summary>
    /// Adds loyalty points to the customer's account based on purchase amount.
    /// </summary>
    /// <param name="purchaseAmount">The amount of the purchase.</param>
    /// <returns>The number of points added.</returns>
    public int AddLoyaltyPoints(decimal purchaseAmount)
    {
        // For example, 1 point per $10 spent
        int pointsToAdd = (int)(purchaseAmount / 10);
        LoyaltyPoints += pointsToAdd;
        return pointsToAdd;
    }

    /// <summary>
    /// Redeems loyalty points for a discount.
    /// </summary>
    /// <param name="pointsToRedeem">The number of points to redeem.</param>
    /// <returns>The discount amount ($1 for every 100 points).</returns>
    /// <exception cref="InvalidOperationException">Thrown when there are insufficient points.</exception>
    public decimal RedeemLoyaltyPoints(int pointsToRedeem)
    {
        if (pointsToRedeem > LoyaltyPoints)
        {
            throw new InvalidOperationException("Insufficient loyalty points");
        }

        LoyaltyPoints -= pointsToRedeem;
        
        // For example, $1 discount for every 100 points
        return pointsToRedeem / 100.0m;
    }
}