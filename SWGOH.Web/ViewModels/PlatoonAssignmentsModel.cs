namespace SWGOH.Web.ViewModels
{
    public class PlatoonAssignmentsModel
    {
        public string CharacterName { get; set; }
        public string AssignedMember { get; set; }
        public string AssignedPlatoon { get; set; }
        public int Stars { get; set; }        
        public int Level { get; set; }
        public int Gear { get; set; }

        public string ShipName { get; set; }
        public string AssignedShipMember { get; set; }
        public string AssignedShipPlatoon { get; set; }
        public int ShipStars { get; set; }
        public int ShipLevel { get; set; }
    }
}