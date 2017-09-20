﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWGOH.Entities
{
    public class MemberShip
    {
        [Required]
        public Guid Id { get; set; }
        public int Level { get; set; }
        public int? Stars { get; set; }
        public string Power { get; set; }
        public Guid Ship_Id { get; set; }
        public Guid Member_Id { get; set; }

        [ForeignKey("Ship_Id")]
        public virtual Ship Ship { get; set; }

        [ForeignKey("Member_Id")]
        public virtual Member Member { get; set; }
    }
}
