using System;
using System.Collections.Generic;

namespace API.Data.Entities;

public partial class VwIndividualCustomer
{
    public int CustomerId { get; set; }

    public string CustomerType { get; set; } = null!;

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public DateOnly? BirthDate { get; set; }

    public string? Email { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Address { get; set; }

    public string? City { get; set; }

    public string? State { get; set; }

    public string? ZipCode { get; set; }

    public DateTime RegistrationDate { get; set; }

    public bool IsEligibleForPromotions { get; set; }

    public string CommunicationPreference { get; set; } = null!;

    public int LoyaltyPoints { get; set; }

    public DateTime? LastPurchaseDate { get; set; }

    public string? LineOfCreditId { get; set; }

    public decimal? CreditLimit { get; set; }

    public decimal? CurrentBalance { get; set; }

    public string? CreditProvider { get; set; }

    public string? CreditStatus { get; set; }
}
