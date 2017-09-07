using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWGOH.Entities
{
    public class TerritoryBattle
    {
        [Required]
        public Guid Id { get; set; }
        public Guid Guild_Id { get; set; }
        public virtual Guild Guild { get; set; }
        public DateTime StartDate { get; set; }
        public TerritoryBattlePhase Phase1 { get; set; }
        public TerritoryBattlePhase Phase2 { get; set; }
        public TerritoryBattlePhase Phase3 { get; set; }
        public TerritoryBattlePhase Phase4 { get; set; }
        public TerritoryBattlePhase Phase5 { get; set; }
        public TerritoryBattlePhase Phase6 { get; set; }
    }
}
