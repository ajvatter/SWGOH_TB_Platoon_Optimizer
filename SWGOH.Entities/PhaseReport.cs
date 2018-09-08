using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWGOH.Entities
{
    public class PhaseReport
    {
        [Required]
        public Guid Id { get; set; }

        public Guid TerritoryBattlePhase_Id { get; set; }

        [ForeignKey("TerritoryBattlePhase_Id")]
        public virtual TerritoryBattlePhase TerritoryBattlePhase { get; set; }

        public Guid? MemberCharacter_Id { get; set; }

        [ForeignKey("MemberCharacter_Id")]
        public virtual MemberCharacter MemberCharacter { get; set; }

        public Guid? MemberShip_Id { get; set; }

        [ForeignKey("MemberShip_Id")]
        public virtual MemberShip MemberShip { get; set; }

        public Guid? PlatoonCharacter_Id { get; set; }

        [ForeignKey("PlatoonCharacter_Id")]
        public virtual PlatoonCharacter PlatoonCharacter { get; set; }

        public Guid? PlatoonShip_Id { get; set; }

        [ForeignKey("PlatoonShip_Id")]
        public virtual PlatoonShip PlatoonShip { get; set; }

        public Guid? GuildId { get; set; }

        [ForeignKey("GuildId")]
        public virtual Guild Guild { get; set; }        
    }
}
