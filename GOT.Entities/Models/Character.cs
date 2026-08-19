using System;
using System.Collections.Generic;

namespace GOT.Entities.Models;

public partial class Character
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public bool IsCommander { get; set; }

    public bool IsKing { get; set; }

    public bool IsDeath { get; set; }

    public bool IsGeneric { get; set; }

    public bool IsHuman { get; set; }

    public bool IsRoyalty { get; set; }

    public virtual ICollection<BattleCommander> BattleCommanders { get; set; } = new List<BattleCommander>();

    public virtual ICollection<BattleKing> BattleKings { get; set; } = new List<BattleKing>();

    public virtual ICollection<Death> DeathKilleds { get; set; } = new List<Death>();

    public virtual ICollection<Death> DeathKillers { get; set; } = new List<Death>();
}
