using System;
using System.Collections.Generic;

namespace API.Data.Models;

public partial class AttributeCategory
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdateAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<Attribute> Attributes { get; set; } = new List<Attribute>();
}
