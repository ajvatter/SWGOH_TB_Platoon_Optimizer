using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWGOH.Entities
{
    public class TerritoryBattlePhase
    {
        [Required]
        public Guid Id { get; set; }

        public int RequiredStars { get; set; }

        public bool HasSecondTerritory { get; set; }

        public int Phase { get; set; }

        public virtual ICollection<PhaseTerritory> PhaseTerritories { get; set; }

        public Guid TerritoryBattle_Id { get; set; }

        [ForeignKey("TerritoryBattle_Id")]
        public virtual TerritoryBattle TerritoryBattle { get; set; }

        //public Guid? Territory1_Id { get; set; }

        //[ForeignKey("Territory1_Id")]
        //public virtual PhaseTerritory Territory1 { get; set; }


        //public Guid? Territory2_Id { get; set; }

        //[ForeignKey("Territory2_Id")]
        //public virtual PhaseTerritory Territory2 { get; set; }      

        //public Guid? TerritoryBattle_Id { get; set; }

        //[ForeignKey("TerritoryBattle_Id")]
        //public virtual TerritoryBattle TerritoryBattle { get; set; }
    }
}
