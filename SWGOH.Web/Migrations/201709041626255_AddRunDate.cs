namespace SWGOH.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRunDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Guilds", "LastScrape", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Guilds", "LastScrape");
        }
    }
}
