namespace SWGOH.Web.Migrations.IdentityMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class guildIdOnUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Guild_Id", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Guild_Id");
        }
    }
}
