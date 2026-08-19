using System;
using System.Collections.Generic;

namespace GOT.Entities.Models;

public partial class Death
{
    public int Id { get; set; }

    public int ChapterId { get; set; }

    public int KilledId { get; set; }

    public int? KillerId { get; set; }

    public int DeathCategoryId { get; set; }

    public string? DeathDescription { get; set; }

    public string? Reason { get; set; }

    public int? LocationId { get; set; }

    public string? LocationComments { get; set; }

    public string? Allegiance { get; set; }

    public int DeathCount { get; set; }

    public virtual Chapter Chapter { get; set; } = null!;

    public virtual DeathCategory DeathCategory { get; set; } = null!;

    public virtual Character Killed { get; set; } = null!;

    public virtual Character? Killer { get; set; }

    public virtual Location? Location { get; set; }
}
