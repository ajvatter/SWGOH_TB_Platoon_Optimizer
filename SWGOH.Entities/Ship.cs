using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWGOH.Entities
{
    public class Ship
    {
        [Required]
        public Guid Id { get; set; }
        [StringLength(255)]
        public string Name { get; set; }
        [StringLength(255)]
        public string DisplayName { get; set; }
        [StringLength(255)]
        public string UrlExt { get; set; }
        public Alignment Alignment { get; set; }

        public virtual ICollection<MemberShip> MemberShips { get; set; }
    }
}
