using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public PhaseTerritory Territory1 { get; set; }
        public PhaseTerritory Territory2 { get; set; }
    }
}
