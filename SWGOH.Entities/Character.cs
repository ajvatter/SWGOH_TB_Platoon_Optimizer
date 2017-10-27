using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SWGOH.Entities
{
    public class Character
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

        public virtual ICollection<MemberCharacter> MemberCharacters { get; set; }
        public virtual ICollection<CharacterClassifier> CharacterClassifiers { get; set; }
    }
    public enum Alignment
    {
        LightSide = 1,
        DarkSide = 2
    }
}
