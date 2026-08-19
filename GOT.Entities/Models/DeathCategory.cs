using System;
using System.Collections.Generic;

namespace GOT.Entities.Models;

public partial class DeathCategory
{
    public int Id { get; set; }

    public string Category { get; set; } = null!;

    public virtual ICollection<Death> Deaths { get; set; } = new List<Death>();
}
