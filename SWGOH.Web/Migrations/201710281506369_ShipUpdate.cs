namespace SWGOH.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ShipUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ships", "BaseId", c => c.String());
            AddColumn("dbo.Ships", "MaxPower", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Ships", "MaxPower");
            DropColumn("dbo.Ships", "BaseId");
        }
    }
}
