namespace SWGOH.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CharacterUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Characters", "BaseId", c => c.String());
            AddColumn("dbo.Characters", "MaxPower", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Characters", "MaxPower");
            DropColumn("dbo.Characters", "BaseId");
        }
    }
}
