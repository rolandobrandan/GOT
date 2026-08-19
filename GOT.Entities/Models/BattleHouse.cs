using System;
using System.Collections.Generic;

namespace GOT.Entities.Models;

public partial class BattleHouse
{
    public int Id { get; set; }

    public int BattleId { get; set; }

    public int HouseId { get; set; }

    public bool IsAttacker { get; set; }

    public virtual Battle Battle { get; set; } = null!;

    public virtual House House { get; set; } = null!;
}
