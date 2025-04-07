using System;
using System.Collections.Generic;

namespace API.Data.Entities;

public partial class LineOfCreditEntity
{
    public int CustomerId { get; set; }

    public decimal CreditLimit { get; set; }

    public decimal CurrentBalance { get; set; }

    public string Id { get; set; } = null!;

    public string Provider { get; set; } = null!;

    public decimal AnnualInterestRate { get; set; }

    public DateTime OpenDate { get; set; }

    public DateTime NextPaymentDueDate { get; set; }

    public decimal MinimumPaymentAmount { get; set; }

    public int CreditScore { get; set; }

    public string Status { get; set; } = null!;

    public DateTime LastReviewDate { get; set; }

    public virtual ICollection<CreditTransactionEntity> CreditTransactionEntities { get; set; } = new List<CreditTransactionEntity>();

    public virtual CustomerEntity Customer { get; set; } = null!;
}
