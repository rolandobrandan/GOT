using System;
using System.Collections.Generic;

namespace GOT.Entities.Models;

public partial class Chapter
{
    public int Id { get; set; }

    public byte SeasonId { get; set; }

    public byte NroInSeason { get; set; }

    public string Title { get; set; } = null!;

    public DateOnly OriginalDateAir { get; set; }

    public decimal UsViewers { get; set; }

    public virtual ICollection<Death> Deaths { get; set; } = new List<Death>();

    public virtual Season Season { get; set; } = null!;
}
