using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SWGOH.Web.ViewModels
{
    public class PlatoonAssignmentsModel
    {
        public string CharacterName { get; set; }
        public string AssignedMember { get; set; }
        public string AssignedPlatoon { get; set; }
        public int Stars { get; set; }
        public int Level { get; set; }
        public int Gear { get; set; }
    }
}