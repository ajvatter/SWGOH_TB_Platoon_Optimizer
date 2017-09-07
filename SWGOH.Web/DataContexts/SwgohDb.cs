using SWGOH.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SWGOH.Web.DataContexts
{
    public class SwgohDb :DbContext
    {
        public SwgohDb()
            : base("SwgohDb")
        {
        }

        public DbSet<Character> Characters { get; set; }
        public DbSet<Guild> Guilds { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<MemberCharacter> MemberCharacters { get; set; }
        public DbSet<TerritoryBattle> TerritoryBattles { get; set; }
        public DbSet<TerritoryBattlePhase> TerritoryBattlePhases { get; set; }
        public DbSet<PhaseTerritory> PhaseTerritories { get; set; }
        public DbSet<TerritoryPlatoon> TerritoryPlatoons { get; set; }
    }
}
