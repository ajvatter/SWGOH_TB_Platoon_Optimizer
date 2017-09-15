using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SWGOH.Web.ViewModels
{
    public class TerritoryBattlePhaseModel
    {
        public Guid Id { get; set; }

        public int RequiredStars { get; set; }

        public bool HasSecondTerritory { get; set; }

        public int Phase { get; set; }

        public PhaseTerritoryModel Territory1 { get; set; }

        public PhaseTerritoryModel Territory2 { get; set; }

        public Guid? TerritoryBattle_Id { get; set; }
    }
}