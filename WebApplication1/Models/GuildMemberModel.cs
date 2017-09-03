using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class GuildMemberModel : BaseModel
    {
        public string Name { get; set; }
        public string UrlExt { get; set; }
        public Guid Guild_Id { get; set; }
    }
}