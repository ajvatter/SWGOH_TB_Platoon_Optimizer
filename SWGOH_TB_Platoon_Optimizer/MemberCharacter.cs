using System;

namespace SWGOH_TB_Platoon_Optimizer
{
    public class MemberCharacter
    {
        public MemberCharacter()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public int? Level { get; set; }
        public string Gear { get; set; }
        public int? Stars { get; set; }
        public string Power { get; set; }
        public Guid CharacterId { get; set; }
    }
}
