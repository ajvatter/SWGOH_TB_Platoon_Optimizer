using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        [ForeignKey("MemberCharacter_Id")]
        public virtual MemberCharacter MemberShip { get; set; }

        public Guid? PlatoonCharacter_Id { get; set; }

        [ForeignKey("PlatoonCharacter_Id")]
        public virtual PlatoonCharacter PlatoonCharacter { get; set; }
    }
}
