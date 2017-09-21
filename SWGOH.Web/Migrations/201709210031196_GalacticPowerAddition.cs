namespace SWGOH.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GalacticPowerAddition : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Members", "ShipPower", c => c.Int(nullable: false));
            AddColumn("dbo.Members", "CharacterPower", c => c.Int(nullable: false));
            AddColumn("dbo.Guilds", "ShipPower", c => c.Int(nullable: false));
            AddColumn("dbo.Guilds", "CharacterPower", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Guilds", "CharacterPower");
            DropColumn("dbo.Guilds", "ShipPower");
            DropColumn("dbo.Members", "CharacterPower");
            DropColumn("dbo.Members", "ShipPower");
        }
    }
}
