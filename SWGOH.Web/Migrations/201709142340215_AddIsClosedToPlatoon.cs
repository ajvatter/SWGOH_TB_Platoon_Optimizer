namespace SWGOH.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsClosedToPlatoon : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TerritoryPlatoons", "IsClosed", c => c.Boolean(nullable: false, defaultValue: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TerritoryPlatoons", "IsClosed");
        }
    }
}
