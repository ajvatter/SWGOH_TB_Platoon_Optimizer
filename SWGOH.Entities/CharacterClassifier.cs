using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SWGOH.Entities
{
    public class CharacterClassifier
    {
        [Required]
        public Guid Id { get; set; }
        [StringLength(255)]
        public string Name { get; set; }

        public virtual ICollection<Character> Characters { get; set; }
    }
}
