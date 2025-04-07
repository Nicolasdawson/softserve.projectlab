using System;
using System.Collections.Generic;

namespace API.Data.Entities;

public partial class VwBusinessCustomer
{
    public int CustomerId { get; set; }

    public string CustomerType { get; set; } = null!;

    public string? ContactFirstName { get; set; }

    public string? ContactLastName { get; set; }

    public string? Email { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Address { get; set; }

    public string? City { get; set; }

    public string? State { get; set; }

    public string? ZipCode { get; set; }

    public DateTime RegistrationDate { get; set; }

    public string CompanyName { get; set; } = null!;

    public string TaxId { get; set; } = null!;

    public string Industry { get; set; } = null!;

    public int EmployeeCount { get; set; }

    public decimal AnnualRevenue { get; set; }

    public string BusinessSize { get; set; } = null!;

    public decimal VolumeDiscountRate { get; set; }

    public string CreditTerms { get; set; } = null!;

    public string? LineOfCreditId { get; set; }

    public decimal? CreditLimit { get; set; }

    public decimal? CurrentBalance { get; set; }

    public string? CreditProvider { get; set; }

    public string? CreditStatus { get; set; }
}
