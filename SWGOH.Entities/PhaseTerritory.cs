using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWGOH.Entities
{
    public class PhaseTerritory
    {
        [Required]
        public Guid Id { get; set; }
        public Guid Character1 { get; set; }
        public Guid Character2 { get; set; }
        public Guid Character3 { get; set; }
        public Guid Character4 { get; set; }
        public Guid Character5 { get; set; }
        public Guid Character6 { get; set; }
        public Guid Character7 { get; set; }
        public Guid Character8 { get; set; }
        public Guid Character9 { get; set; }
        public Guid Character10 { get; set; }
        public Guid Character11 { get; set; }
        public Guid Character12 { get; set; }
        public Guid Character13 { get; set; }
        public Guid Character14 { get; set; }
        public Guid Character15 { get; set; }
    }
}
