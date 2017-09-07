using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWGOH.Entities
{
    public class TerritoryPlatoon
    {
        [Required]
        public Guid Id { get; set; }
        public Character Character1 { get; set; }
        public Character Character2 { get; set; }
        public Character Character3 { get; set; }
        public Character Character4 { get; set; }
        public Character Character5 { get; set; }
        public Character Character6 { get; set; }
        public Character Character7 { get; set; }
        public Character Character8 { get; set; }
        public Character Character9 { get; set; }
        public Character Character10 { get; set; }
        public Character Character11 { get; set; }
        public Character Character12 { get; set; }
        public Character Character13 { get; set; }
        public Character Character14 { get; set; }
        public Character Character15 { get; set; }
    }
}
