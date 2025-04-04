using Microsoft.AspNetCore.Authentication.Cookies;
using System;
using System.Collections.Generic;

namespace API.Data.Entities;

public partial class BranchEntity
{
    public int BranchId { get; set; }
    public string BranchName { get; set; }
    public string BranchCity { get; set; }
    public string BranchAddress { get; set; }
    public string BranchRegion { get; set; }
    public string BranchContactNumber { get; set; }
    public string BranchContactEmail { get; set; }

    public virtual ICollection<UsersEntity> UsersEntities { get; set; } = new List<UsersEntity>();
    public virtual ICollection<WarehouseEntity> WarehouseEntities { get; set; } = new List<WarehouseEntity>();
    public virtual ICollection<UserEntity> UserEntities { get; set; } = new List<UserEntity>();
}
