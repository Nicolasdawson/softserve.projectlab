using System;
using System.Collections.Generic;

namespace API.Data.Models;

public partial class Attribute : BaseClass
{
    public int Id { get; set; }

    public int? IdProduct { get; set; }

    public int? IdAttributeCategory { get; set; }

    public string? Field { get; set; }

    public string? Value { get; set; }

    public virtual AttributeCategory? IdAttributeCategoryNavigation { get; set; }

    public virtual Product? IdProductNavigation { get; set; }
}
