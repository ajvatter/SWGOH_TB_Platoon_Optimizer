using SWGOH.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SWGOH.Web.ViewModels
{
    public class TerritoryPlatoonModel
    {
        public Guid Id { get; set; }

        public int PlatoonNumber { get; set; }

        public TerritoryBattlePhase Phase { get; set; }

        public PhaseTerritory Territory { get; set; }

        public bool IsClosed { get; set; }

        public PlatoonCharacter Character1 { get; set; }

        public PlatoonCharacter Character2 { get; set; }

        public PlatoonCharacter Character3 { get; set; }

        public PlatoonCharacter Character4 { get; set; }

        public PlatoonCharacter Character5 { get; set; }

        public PlatoonCharacter Character6 { get; set; }

        public PlatoonCharacter Character7 { get; set; }

        public PlatoonCharacter Character8 { get; set; }

        public PlatoonCharacter Character9 { get; set; }

        public PlatoonCharacter Character10 { get; set; }

        public PlatoonCharacter Character11 { get; set; }

        public PlatoonCharacter Character12 { get; set; }

        public PlatoonCharacter Character13 { get; set; }

        public PlatoonCharacter Character14 { get; set; }

        public PlatoonCharacter Character15 { get; set; }

        public IEnumerable<Character> Items { get; set; }
    }
}