using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SWGOH.Entities
{
    public class Guild
    {
        [Required]
        public Guid Id { get; set; }
        [StringLength(255)]
        public string Name { get; set; }
        [StringLength(255)]
        public string UrlExt { get; set; }
        public DateTime LastScrape { get; set; }
        public int ShipPower { get; set; }
        public int CharacterPower { get; set; }

        public virtual ICollection<Member> Members { get; set; }
    }
}
