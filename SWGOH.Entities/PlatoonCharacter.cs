using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWGOH.Entities
{
    public class PlatoonCharacter
    {
        [Required]
        public Guid Id { get; set; }

        public int PlatoonPosition { get; set; }

        public Guid? Character_Id { get; set; }

        [ForeignKey("Character_Id")]
        public virtual Character Character { get; set; }

        public Guid TerritoryPlatoon_Id { get; set; }

        [ForeignKey("TerritoryPlatoon_Id")]
        public virtual TerritoryPlatoon TerritoryPlatoon { get; set; }

        //public Guid? Member_Id { get; set; }

        //[ForeignKey("Member_Id")]
        public virtual Member Member { get; set; }
    }
}
