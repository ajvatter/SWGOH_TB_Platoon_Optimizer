namespace SWGOH.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DisplayNames : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Characters", "DisplayName", c => c.String(maxLength: 255));
            AddColumn("dbo.Members", "DisplayName", c => c.String(maxLength: 255));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Members", "DisplayName");
            DropColumn("dbo.Characters", "DisplayName");
        }
    }
}
