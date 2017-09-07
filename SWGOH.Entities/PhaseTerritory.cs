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
        public TerritoryPlatoon TerritoryPlatoon1 { get; set; }
        public TerritoryPlatoon TerritoryPlatoon2 { get; set; }
        public TerritoryPlatoon TerritoryPlatoon3 { get; set; }
        public TerritoryPlatoon TerritoryPlatoon4 { get; set; }
        public TerritoryPlatoon TerritoryPlatoon5 { get; set; }
        public TerritoryPlatoon TerritoryPlatoon6 { get; set; }
    }
}
