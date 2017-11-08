using SWGOH.Entities;
using System;

namespace SWGOH.Web.ViewModels
{
    public class TerritoryBattleResultsModel
    {
        public Guid Id { get; set; }        
        public DateTime StartDate { get; set; }        
        public int Phase1Points { get; set; }
        public int Phase2TopCharPoints { get; set; }
        public int Phase2BottomCharPoints { get; set; }
        public int Phase3FleetPoints { get; set; }
        public int Phase3TopCharPoints { get; set; }
        public int Phase3BottomCharPoints { get; set; }
        public int Phase4FleetPoints { get; set; }
        public int Phase4TopCharPoints { get; set; }
        public int Phase4BottomCharPoints { get; set; }
        public int Phase5FleetPoints { get; set; }
        public int Phase5TopCharPoints { get; set; }
        public int Phase5BottomCharPoints { get; set; }
        public int Phase6FleetPoints { get; set; }
        public int Phase6TopCharPoints { get; set; }
        public int Phase6BottomCharPoints { get; set; }
    }
}