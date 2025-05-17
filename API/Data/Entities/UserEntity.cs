using System;
using System.Collections.Generic;

namespace API.Data.Entities;

public partial class UserEntity : BaseEntity
{
    public int UserId { get; set; }

    public string UserFirstName { get; set; } = null!;

    public string? UserLastName { get; set; }

    public string UserContactEmail { get; set; } = null!;

    public string UserContactNumber { get; set; } = null!;

    public byte[] PasswordHash { get; set; } = null!;
    public byte[] PasswordSalt { get; set; } = null!;

    public bool UserStatus { get; set; }

    public int BranchId { get; set; }

    public virtual BranchEntity Branch { get; set; } = null!;

    public virtual ICollection<UserRoleEntity> UserRoleEntities { get; set; } = new List<UserRoleEntity>();
}
