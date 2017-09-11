using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWGOH.Entities
{
    public class PhaseTerritory
    {
        [Required]
        public Guid Id { get; set; }

        public int? TotalPointsEarned { get; set; }

        public Guid? TerritoryPlatoon1_Id { get; set; }

        [ForeignKey("TerritoryPlatoon1_Id")]
        public virtual TerritoryPlatoon TerritoryPlatoon1 { get; set; }

        public Guid? TerritoryPlatoon2_Id { get; set; }

        [ForeignKey("TerritoryPlatoon2_Id")]
        public virtual TerritoryPlatoon TerritoryPlatoon2 { get; set; }

        public Guid? TerritoryPlatoon3_Id { get; set; }

        [ForeignKey("TerritoryPlatoon3_Id")]
        public virtual TerritoryPlatoon TerritoryPlatoon3 { get; set; }

        public Guid? TerritoryPlatoon4_Id { get; set; }

        [ForeignKey("TerritoryPlatoon4_Id")]
        public virtual TerritoryPlatoon TerritoryPlatoon4 { get; set; }

        public Guid? TerritoryPlatoon5_Id { get; set; }

        [ForeignKey("TerritoryPlatoon5_Id")]
        public virtual TerritoryPlatoon TerritoryPlatoon5 { get; set; }

        public Guid? TerritoryPlatoon6_Id { get; set; }

        [ForeignKey("TerritoryPlatoon6_Id")]
        public virtual TerritoryPlatoon TerritoryPlatoon6 { get; set; }

        public Guid TerritoryBattle_Id { get; set; }

        [ForeignKey("TerritoryBattle_Id")]
        public virtual TerritoryBattle TerritoryBattle { get; set; }
    }
}
