using SWGOH.Entities;
using System;

namespace SWGOH.Web.ViewModels
{
    public class TerritoryBattleModel
    {
        public Guid Id { get; set; }
        public virtual Guild Guild { get; set; }
        public DateTime StartDate { get; set; }
        public int? TotalStars { get; set; }
        public bool IsActive { get; set; }
        public TerritoryBattlePhaseModel Phase1 { get; set; }
        public TerritoryBattlePhaseModel Phase2 { get; set; }
        public TerritoryBattlePhaseModel Phase3 { get; set; }
        public TerritoryBattlePhaseModel Phase4 { get; set; }
        public TerritoryBattlePhaseModel Phase5 { get; set; }
        public TerritoryBattlePhaseModel Phase6 { get; set; }
    }
}