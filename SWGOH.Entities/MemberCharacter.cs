using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWGOH.Entities
{
    public class MemberCharacter
    {
        [Required]
        public Guid Id { get; set; }
        public int Level { get; set; }
        public int Gear { get; set; }
        public int? Stars { get; set; }
        public int Power { get; set; }
        public Guid Character_Id { get; set; }
        public Guid Member_Id { get; set; }

        [ForeignKey("Character_Id")]
        public virtual Character Character { get; set; }

        [ForeignKey("Member_Id")]
        public virtual Member Member { get; set; }
    }
}
