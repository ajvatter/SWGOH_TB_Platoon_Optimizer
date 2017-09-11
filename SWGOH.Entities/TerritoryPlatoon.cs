﻿using System;
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

        //public ICollection<PlatoonCharacter> PlatoonCharacters { get; set; }

        public Guid? Character1_Id { get; set; }

        [ForeignKey("Character1_Id")]
        public virtual Character Character1 { get; set; }

        public Guid? Character2_Id { get; set; }

        [ForeignKey("Character2_Id")]
        public virtual Character Character2 { get; set; }

        public Guid? Character3_Id { get; set; }

        [ForeignKey("Character3_Id")]
        public virtual Character Character3 { get; set; }

        public Guid? Character4_Id { get; set; }

        [ForeignKey("Character4_Id")]
        public virtual Character Character4 { get; set; }

        public Guid? Character5_Id { get; set; }

        [ForeignKey("Character5_Id")]
        public virtual Character Character5 { get; set; }

        public Guid? Character6_Id { get; set; }

        [ForeignKey("Character6_Id")]
        public virtual Character Character6 { get; set; }

        public Guid? Character7_Id { get; set; }

        [ForeignKey("Character7_Id")]
        public virtual Character Character7 { get; set; }

        public Guid? Character8_Id { get; set; }

        [ForeignKey("Character8_Id")]
        public virtual Character Character8 { get; set; }

        public Guid? Character9_Id { get; set; }

        [ForeignKey("Character9_Id")]
        public virtual Character Character9 { get; set; }

        public Guid? Character10_Id { get; set; }

        [ForeignKey("Character10_Id")]
        public virtual Character Character10 { get; set; }

        public Guid? Character11_Id { get; set; }

        [ForeignKey("Character11_Id")]
        public virtual Character Character11 { get; set; }

        public Guid? Character12_Id { get; set; }

        [ForeignKey("Character12_Id")]
        public virtual Character Character12 { get; set; }

        public Guid? Character13_Id { get; set; }

        [ForeignKey("Character13_Id")]
        public virtual Character Character13 { get; set; }

        public Guid? Character14_Id { get; set; }

        [ForeignKey("Character14_Id")]
        public virtual Character Character14 { get; set; }

        public Guid? Character15_Id { get; set; }

        [ForeignKey("Character15_Id")]
        public virtual Character Character15 { get; set; }

        public Guid? Character1Member_Id { get; set; }

        [ForeignKey("Character1Member_Id")]
        public virtual Member Character1Member { get; set; }

        public Guid? Character2Member_Id { get; set; }

        [ForeignKey("Character2Member_Id")]
        public virtual Member Character2Member { get; set; }

        public Guid? Character3Member_Id { get; set; }

        [ForeignKey("Character3Member_Id")]
        public virtual Member Character3Member { get; set; }

        public Guid? Character4Member_Id { get; set; }

        [ForeignKey("Character4Member_Id")]
        public virtual Member Character4Member { get; set; }

        public Guid? Character5Member_Id { get; set; }

        [ForeignKey("Character5Member_Id")]
        public virtual Member Character5Member { get; set; }

        public Guid? Character6Member_Id { get; set; }

        [ForeignKey("Character6Member_Id")]
        public virtual Member Character6Member { get; set; }

        public Guid? Character7Member_Id { get; set; }

        [ForeignKey("Character7Member_Id")]
        public virtual Member Character7Member { get; set; }

        public Guid? Character8Member_Id { get; set; }

        [ForeignKey("Character8Member_Id")]
        public virtual Member Character8Member { get; set; }

        public Guid? Character9Member_Id { get; set; }

        [ForeignKey("Character9Member_Id")]
        public virtual Member Character9Member { get; set; }

        public Guid? Character10Member_Id { get; set; }

        [ForeignKey("Character10Member_Id")]
        public virtual Member Character10Member { get; set; }

        public Guid? Character11Member_Id { get; set; }

        [ForeignKey("Character11Member_Id")]
        public virtual Member Character11Member { get; set; }

        public Guid? Character12Member_Id { get; set; }

        [ForeignKey("Character12Member_Id")]
        public virtual Member Character12Member { get; set; }

        public Guid? Character13Member_Id { get; set; }

        [ForeignKey("Character13Member_Id")]
        public virtual Member Character13Member { get; set; }

        public Guid? Character14Member_Id { get; set; }

        [ForeignKey("Character14Member_Id")]
        public virtual Member Character14Member { get; set; }

        public Guid? Character15Member_Id { get; set; }

        [ForeignKey("Character15Member_Id")]
        public virtual Member Character15Member { get; set; }

        public Guid TerritoryBattle_Id { get; set; }

        [ForeignKey("TerritoryBattle_Id")]
        public virtual TerritoryBattle TerritoryBattle { get; set; }
    }
}