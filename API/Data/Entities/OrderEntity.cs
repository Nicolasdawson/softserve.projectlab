using System;
using System.Collections.Generic;

namespace API.Data.Entities;

public partial class OrderEntity
{
    public int OrderId { get; set; }

    public int? CustomerId { get; set; }

    public decimal? TotalAmount { get; set; }

    public string? Status { get; set; }

    public DateTime? OrderDate { get; set; }

    public virtual CustomerEntity? Customer { get; set; }
}
