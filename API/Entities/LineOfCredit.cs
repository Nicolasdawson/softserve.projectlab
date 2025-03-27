using System;
using System.Collections.Generic;

namespace API.Entities;

public partial class LineOfCredit
{
    public int CustomerId { get; set; }

    public decimal? CreditLimit { get; set; }

    public decimal? CurrentBalance { get; set; }

    public virtual Customer Customer { get; set; } = null!;
}
