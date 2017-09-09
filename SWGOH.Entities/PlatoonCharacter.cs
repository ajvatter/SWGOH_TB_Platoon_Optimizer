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

        public Guid Character_Id { get; set; }

        [ForeignKey("Character_Id")]
        public virtual Character Character { get; set; }

        public int QtyRequired { get; set; }
    }
}
