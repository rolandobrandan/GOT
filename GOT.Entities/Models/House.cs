using System;
using System.Collections.Generic;

namespace GOT.Entities.Models;

public partial class House
{
    public int Id { get; set; }

    public int? KingdomId { get; set; }

    public string Name { get; set; } = null!;

    public string? Url { get; set; }

    public string? Summary { get; set; }

    public string? CoatOfArmsUrl { get; set; }

    public virtual ICollection<BattleHouse> BattleHouses { get; set; } = new List<BattleHouse>();

    public virtual Kingdom? Kingdom { get; set; }
}
