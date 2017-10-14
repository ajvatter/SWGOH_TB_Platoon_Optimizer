using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWGOH.Entities
{
    public class TerritoryBattle
    {
        [Required]
        public Guid Id { get; set; }

        public Guid Guild_Id { get; set; }

        [ForeignKey("Guild_Id")]
        public virtual Guild Guild { get; set; }

        public DateTime StartDate { get; set; }

        public int? TotalStars { get; set; }
        
        public bool IsActive { get; set; }

        public virtual ICollection<TerritoryBattlePhase> TerritoryBattlePhases { get; set; }        
    }
}
