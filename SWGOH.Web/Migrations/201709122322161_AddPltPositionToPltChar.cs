namespace SWGOH.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPltPositionToPltChar : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PlatoonCharacters", "PlatoonPosition", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PlatoonCharacters", "PlatoonPosition");
        }
    }
}
