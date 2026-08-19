using System;
using System.Collections.Generic;

namespace GOT.Entities.Models;

public partial class Season
{
    public byte Id { get; set; }

    public string Name { get; set; } = null!;

    public int Year { get; set; }

    public virtual ICollection<Chapter> Chapters { get; set; } = new List<Chapter>();
}
