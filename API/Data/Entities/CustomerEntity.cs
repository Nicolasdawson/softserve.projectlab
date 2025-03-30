using System;
using System.Collections.Generic;

namespace API.Data.Entities;

public partial class CustomerEntity
{
    public int CustomerId { get; set; }

    public string? CustomerType { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public virtual ICollection<CartEntity> CartEntities { get; set; } = new List<CartEntity>();

    public virtual LineOfCreditEntity? LineOfCreditEntity { get; set; }

    public virtual ICollection<OrderEntity> OrderEntities { get; set; } = new List<OrderEntity>();
}
