namespace SWGOH.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RewriteFixes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PlatoonCharacters", "Member_Id", c => c.Guid(nullable: false));
            CreateIndex("dbo.PlatoonCharacters", "Member_Id");
            AddForeignKey("dbo.PlatoonCharacters", "Member_Id", "dbo.Members", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PlatoonCharacters", "Member_Id", "dbo.Members");
            DropIndex("dbo.PlatoonCharacters", new[] { "Member_Id" });
            DropColumn("dbo.PlatoonCharacters", "Member_Id");
        }
    }
}
