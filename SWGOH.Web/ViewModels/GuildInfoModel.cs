using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SWGOH.Web.ViewModels
{
    public class GuildInfoModel
    {
        public string Name { get; set; }
        public int MemberCount { get; set; }
        public Guid Guild_Id { get; set; }
        public int GlacticPower { get; set; }
        public int CharacterPower { get; set; }
        public int ShipPower { get; set; }
        public DateTime LastScrape { get; set; }
    }
}