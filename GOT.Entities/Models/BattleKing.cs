using System;
using System.Collections.Generic;

namespace GOT.Entities.Models;

public partial class BattleKing
{
    public int Id { get; set; }

    public int BattleId { get; set; }

    public int? KingId { get; set; }

    public bool IsAttacker { get; set; }

    public virtual Battle Battle { get; set; } = null!;

    public virtual Character? King { get; set; }
}
