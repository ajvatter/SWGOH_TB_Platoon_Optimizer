using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SWGOH.Web.ViewModels
{
    public class PlatoonAssignmentsByCharacter
    {
        public string CharacterName { get; set; }
        public int NeededCount { get; set; }
        public int HaveCount { get; set; }
        public string AssignedMembers { get; set; }
        public string AssignedPlatoons { get; set; }
    }
}