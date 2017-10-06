using SWGOH.Entities;
using System;

namespace SWGOH.Web.ViewModels
{
    public class MemberModel
    {
        public Guid Id { get; set; }
        public string DisplayName { get; set; }
        public string UrlExt { get; set; }
        public int ShipPower { get; set; }
        public int CharacterPower { get; set; }
        public int GalacticPower { get; set; }
    }
}