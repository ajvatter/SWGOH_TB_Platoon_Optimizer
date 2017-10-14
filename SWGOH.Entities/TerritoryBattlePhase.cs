using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWGOH.Entities
{
    public class TerritoryBattlePhase
    {
        [Required]
        public Guid Id { get; set; }

        public int RequiredStars { get; set; }

        public bool HasSecondTerritory { get; set; }

        public bool HasThirdTerritory { get; set; }

        public int Phase { get; set; }

        public virtual ICollection<PhaseTerritory> PhaseTerritories { get; set; }

        public Guid TerritoryBattle_Id { get; set; }

        [ForeignKey("TerritoryBattle_Id")]
        public virtual TerritoryBattle TerritoryBattle { get; set; }      
    }
}
