using System;
using System.Collections.Generic;

namespace API.Data.Models;

public partial class UserAddress
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public string? Address1 { get; set; }

    public string? Address2 { get; set; }

    public string? PostalCode { get; set; }

    public string? Country { get; set; }

    public string? State { get; set; }

    public string? City { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdateAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public bool? IsActive { get; set; }

    public virtual User? User { get; set; }
}
