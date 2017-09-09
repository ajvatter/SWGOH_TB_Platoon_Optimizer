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

        public Guid? Territory1_Id { get; set; }

        [ForeignKey("Territory1_Id")]
        public PhaseTerritory Territory1 { get; set; }

        public bool HasSecondTerritory { get; set; }

        public Guid? Territory2_Id { get; set; }

        [ForeignKey("Territory2_Id")]
        public PhaseTerritory Territory2 { get; set; }        
    }
}
