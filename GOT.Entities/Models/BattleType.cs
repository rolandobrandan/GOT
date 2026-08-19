using System;
using System.Collections.Generic;

namespace GOT.Entities.Models;

public partial class BattleType
{
    public int Id { get; set; }

    public string? BattleType1 { get; set; }

    public virtual ICollection<Battle> Battles { get; set; } = new List<Battle>();
}
