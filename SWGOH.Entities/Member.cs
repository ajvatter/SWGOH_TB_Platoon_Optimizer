using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWGOH.Entities
{
    public class Member
    {
        [Required]
        public Guid Id { get; set; }
        [StringLength(255)]
        public string Name { get; set; }
        [StringLength(255)]
        public string DisplayName { get; set; }
        [StringLength(255)]
        public string UrlExt { get; set; }
        public Guid Guild_Id { get; set; }
        public int ShipPower { get; set; }
        public int CharacterPower { get; set; }

        [ForeignKey("Guild_Id")]
        public virtual Guild Guild { get; set; }

        public virtual ICollection<MemberCharacter> MemberCharacters { get; set; }
    }
}
