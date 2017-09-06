using SWGOH.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SWGOH.Web.ViewModels
{
    public class MembersWithCharacter
    {
        public Character Character { get; set; }

        public virtual IList<MemberCharacter> MembersCharacters { get; set; }
    }
}