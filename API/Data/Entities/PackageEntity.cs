using System;
using System.Collections.Generic;

namespace API.Data.Entities;

public partial class PackageEntity
{
    public int PackageId { get; set; }

    public string PackageName { get; set; } = null!;

    public virtual ICollection<PackageItemEntity> PackageItemEntities { get; set; } = new List<PackageItemEntity>();
}
