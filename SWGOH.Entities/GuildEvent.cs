using System;
using System.ComponentModel.DataAnnotations;

namespace SWGOH.Entities
{
    public class GuildEvent
    {
        [Required]
        public Guid Id { get; set; }
        [StringLength(255)]
        public string Name { get; set; }
        [StringLength(255)]
        public string Type { get; set; }
        public Alignment Alignment { get; set; }
    }
}
