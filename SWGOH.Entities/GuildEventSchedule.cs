using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWGOH.Entities
{
    public class GuildEventSchedule
    {
        [Required]
        public Guid Id { get; set; }
        public DateTime StartDate { get; set; }
        public Guid GuildEvent_Id { get; set; }

        [ForeignKey("GuildEvent_Id")]
        public virtual GuildEvent GuildEvent { get; set; }
    }
}
