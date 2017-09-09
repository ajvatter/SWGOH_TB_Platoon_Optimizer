using System;
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

        public Guid? Phase1_Id { get; set; }

        [ForeignKey("Phase1_Id")]
        public TerritoryBattlePhase Phase1 { get; set; }

        public Guid? Phase2_Id { get; set; }

        [ForeignKey("Phase2_Id")]
        public TerritoryBattlePhase Phase2 { get; set; }

        public Guid? Phase3_Id { get; set; }

        [ForeignKey("Phase3_Id")]
        public TerritoryBattlePhase Phase3 { get; set; }

        public Guid? Phase4_Id { get; set; }

        [ForeignKey("Phase4_Id")]
        public TerritoryBattlePhase Phase4 { get; set; }

        public Guid? Phase5_Id { get; set; }

        [ForeignKey("Phase5_Id")]
        public TerritoryBattlePhase Phase5 { get; set; }

        public Guid? Phase6_Id { get; set; }

        [ForeignKey("Phase6_Id")]
        public TerritoryBattlePhase Phase6 { get; set; }
    }
}
