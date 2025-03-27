using System;
using System.Collections.Generic;

namespace API.Entities;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string? CustomerType { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual LineOfCredit? LineOfCredit { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
