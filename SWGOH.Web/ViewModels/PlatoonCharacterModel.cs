using SWGOH.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SWGOH.Web.ViewModels
{
    public class PlatoonCharacterModel
    {
        public Guid Id { get; set; }

        public Guid Character_Id { get; set; }

        public Character Character { get; set; }

        public Member Member { get; set; }
    }
}