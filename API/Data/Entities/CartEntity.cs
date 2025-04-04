using System;
using System.Collections.Generic;

namespace API.Data.Entities;

public partial class CartEntity
{
    public int CartId { get; set; }

    public int CustomerId { get; set; }

    public virtual ICollection<CartItemEntity> CartItemEntities { get; set; } = new List<CartItemEntity>();

    public virtual CustomerEntity Customer { get; set; } = null!;
}
