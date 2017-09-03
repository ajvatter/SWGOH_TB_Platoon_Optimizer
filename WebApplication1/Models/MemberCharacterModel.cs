using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class MemberCharacterModel : BaseModel
    {
        public int Level { get; set; }
        public string Gear { get; set; }
        public int Stars { get; set; }
        public string Power { get; set; }
        public Guid Character_Id { get; set; }
    }
}