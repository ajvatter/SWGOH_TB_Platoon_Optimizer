using SWGOH.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SWGOH.Web.ViewModels
{
    public class MembersWithShip
    {
        public Ship Ship { get; set; }

        public virtual IList<MemberShip> MembersShips { get; set; }
    }
}