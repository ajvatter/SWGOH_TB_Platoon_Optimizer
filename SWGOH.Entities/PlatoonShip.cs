using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWGOH.Entities
{
    public class PlatoonShip
    {
        [Required]
        public Guid Id { get; set; }

        public int PlatoonPosition { get; set; }

        public Guid? Ship_Id { get; set; }

        [ForeignKey("Ship_Id")]
        public virtual Ship Ship { get; set; }

        public Guid TerritoryPlatoon_Id { get; set; }

        [ForeignKey("TerritoryPlatoon_Id")]
        public virtual TerritoryPlatoon TerritoryPlatoon { get; set; }
    }
}
