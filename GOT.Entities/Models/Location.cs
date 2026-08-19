using System;
using System.Collections.Generic;

namespace GOT.Entities.Models;

public partial class Location
{
    public int Id { get; set; }

    public int? KingdomId { get; set; }

    public string Location1 { get; set; } = null!;

    public string? Url { get; set; }

    public string? Summary { get; set; }

    public virtual ICollection<Battle> Battles { get; set; } = new List<Battle>();

    public virtual ICollection<Death> Deaths { get; set; } = new List<Death>();

    public virtual Kingdom? Kingdom { get; set; }
}
