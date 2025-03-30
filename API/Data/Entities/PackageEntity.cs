using System;
using System.Collections.Generic;

namespace API.Data.Entities;

public partial class PackageEntity
{
    public int PackageId { get; set; }

    public string? PackageName { get; set; }
}
