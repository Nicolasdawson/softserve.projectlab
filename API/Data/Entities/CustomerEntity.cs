using System;
using System.Collections.Generic;

namespace API.Data.Entities;

public partial class CustomerEntity
{
    public int CustomerId { get; set; }

    public string CustomerType { get; set; } = null!;

    public string CustomerName { get; set; } = null!;

    public string CustomerContactNumber { get; set; } = null!;

    public string CustomerContactEmail { get; set; } = null!;

    public virtual ICollection<CartEntity> CartEntities { get; set; } = new List<CartEntity>();

    public virtual LineOfCreditEntity? LineOfCreditEntity { get; set; }

    public virtual ICollection<OrderEntity> OrderEntities { get; set; } = new List<OrderEntity>();
}
