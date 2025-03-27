using System;
using System.Collections.Generic;

namespace API.Entities;

public partial class User
{
    public int UserId { get; set; }

    public string? UserEmail { get; set; }

    public string? UserFirstName { get; set; }

    public string? UserLastName { get; set; }

    public string? UserPassword { get; set; }

    public bool? UserStatus { get; set; }

    public bool? IsBlocked { get; set; }

    public string? Username { get; set; }

    public int? BranchId { get; set; }

    public virtual Branch? Branch { get; set; }

    public virtual UserRole? UserRole { get; set; }
}
