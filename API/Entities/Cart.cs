using System;
using System.Collections.Generic;

namespace API.Entities;

public partial class Cart
{
    public int CartId { get; set; }

    public int? CustomerId { get; set; }

    public virtual Customer? Customer { get; set; }
}
