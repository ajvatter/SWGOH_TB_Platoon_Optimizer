using SWGOH.Entities;
using System;

namespace SWGOH.Web.ViewModels
{
    public class TerritoryBattleIndexModel
    {
        public Guid Id { get; set; }
        public virtual Guild Guild { get; set; }
        public DateTime StartDate { get; set; }
        public int? TotalStars { get; set; }
        public bool IsActive { get; set; }
        public Guid Phase1_Id { get; set; }
        public Guid Phase2_Id { get; set; }
        public Guid Phase3_Id { get; set; }
        public Guid Phase4_Id { get; set; }
        public Guid Phase5_Id { get; set; }
        public Guid Phase6_Id { get; set; }
    }
}