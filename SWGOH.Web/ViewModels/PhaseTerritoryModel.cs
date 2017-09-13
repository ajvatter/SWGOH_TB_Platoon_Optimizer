using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SWGOH.Web.ViewModels
{
    public class PhaseTerritoryModel
    {
        public Guid Id { get; set; }

        public int? TotalPointsEarned { get; set; }

        public string PhaseLocation { get; set; }

        public TerritoryPlatoonModel TerritoryPlatoon1 { get; set; }

        public TerritoryPlatoonModel TerritoryPlatoon2 { get; set; }

        public TerritoryPlatoonModel TerritoryPlatoon3 { get; set; }

        public TerritoryPlatoonModel TerritoryPlatoon4 { get; set; }

        public TerritoryPlatoonModel TerritoryPlatoon5 { get; set; }

        public TerritoryPlatoonModel TerritoryPlatoon6 { get; set; }
    }
}