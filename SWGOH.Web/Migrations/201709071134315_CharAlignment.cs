namespace SWGOH.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CharAlignment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Characters", "Alignment", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Characters", "Alignment");
        }
    }
}
