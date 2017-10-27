using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWGOH.Entities
{
    public class PhaseTerritory
    {
        [Required]
        public Guid Id { get; set; }

        public int? TotalPointsEarned { get; set; }

        public string PhaseLocation{ get; set; }

        public virtual ICollection<TerritoryPlatoon> TerritoryPlatoons { get; set; }

        public Guid TerritoryBattlePhase_Id { get; set; }

        public int? PointsEarned { get; set; }

        [ForeignKey("TerritoryBattlePhase_Id")]
        public virtual TerritoryBattlePhase TerritoryBattlePhase { get; set; }
    }
}
