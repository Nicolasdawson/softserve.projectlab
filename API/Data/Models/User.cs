using System;
using System.Collections.Generic;

namespace API.Data.Models;

public partial class User
{
    public int Id { get; set; }

    public string? User1 { get; set; }

    public string? Password { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string? Name { get; set; }

    public string? Lastname { get; set; }

    public string? LastUsedPaymentType { get; set; }

    public int? LastUsedPayment { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdateAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<UserAddress> UserAddresses { get; set; } = new List<UserAddress>();

    public virtual ICollection<UserCardPayment> UserCardPayments { get; set; } = new List<UserCardPayment>();

    public virtual ICollection<UserPaypalPayment> UserPaypalPayments { get; set; } = new List<UserPaypalPayment>();
}
