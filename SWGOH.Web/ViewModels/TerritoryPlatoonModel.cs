using SWGOH.Entities;
using System;
using System.Collections.Generic;

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
        public PlatoonShip Ship1 { get; set; }
        public PlatoonShip Ship2 { get; set; }
        public PlatoonShip Ship3 { get; set; }
        public PlatoonShip Ship4 { get; set; }
        public PlatoonShip Ship5 { get; set; }
        public PlatoonShip Ship6 { get; set; }
        public PlatoonShip Ship7 { get; set; }
        public PlatoonShip Ship8 { get; set; }
        public PlatoonShip Ship9 { get; set; }
        public PlatoonShip Ship10 { get; set; }
        public PlatoonShip Ship11 { get; set; }
        public PlatoonShip Ship12 { get; set; }
        public PlatoonShip Ship13 { get; set; }
        public PlatoonShip Ship14 { get; set; }
        public PlatoonShip Ship15 { get; set; }
        public IEnumerable<Ship> Ships { get; set; }
    }
}