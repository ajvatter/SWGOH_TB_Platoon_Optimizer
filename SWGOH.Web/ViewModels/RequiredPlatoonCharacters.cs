using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SWGOH.Web.ViewModels
{
    public class RequiredPlatoonCharacters
    {
        public Guid? PlatoonCharacter_Id { get; set; }
        public Guid? TerritoryBattlePhase_Id { get; set; }
        public Guid? MemberCharacter_Id { get; set; }
    }
}