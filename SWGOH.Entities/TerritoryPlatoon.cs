using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWGOH.Entities
{
    public class TerritoryPlatoon
    {
        [Required]
        public Guid Id { get; set; }

        public int PlatoonNumber { get; set; }

        public ICollection<PlatoonCharacter> PlatoonCharacters { get; set; }

        public ICollection<PlatoonShip> PlatoonShips { get; set; }

        public Guid PhaseTerritory_Id { get; set; }

        [ForeignKey("PhaseTerritory_Id")]
        public virtual PhaseTerritory PhaseTerritory { get; set; }

        public bool IsClosed { get; set; }     
    }
}
