namespace SWGOH.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MadeIdsNullableOnPltChar : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PlatoonCharacters", "Character_Id", "dbo.Characters");
            DropForeignKey("dbo.PlatoonCharacters", "Member_Id", "dbo.Members");
            DropIndex("dbo.PlatoonCharacters", new[] { "Character_Id" });
            DropIndex("dbo.PlatoonCharacters", new[] { "Member_Id" });
            AlterColumn("dbo.PlatoonCharacters", "Character_Id", c => c.Guid());
            AlterColumn("dbo.PlatoonCharacters", "Member_Id", c => c.Guid());
            CreateIndex("dbo.PlatoonCharacters", "Character_Id");
            CreateIndex("dbo.PlatoonCharacters", "Member_Id");
            AddForeignKey("dbo.PlatoonCharacters", "Character_Id", "dbo.Characters", "Id");
            AddForeignKey("dbo.PlatoonCharacters", "Member_Id", "dbo.Members", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PlatoonCharacters", "Member_Id", "dbo.Members");
            DropForeignKey("dbo.PlatoonCharacters", "Character_Id", "dbo.Characters");
            DropIndex("dbo.PlatoonCharacters", new[] { "Member_Id" });
            DropIndex("dbo.PlatoonCharacters", new[] { "Character_Id" });
            AlterColumn("dbo.PlatoonCharacters", "Member_Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.PlatoonCharacters", "Character_Id", c => c.Guid(nullable: false));
            CreateIndex("dbo.PlatoonCharacters", "Member_Id");
            CreateIndex("dbo.PlatoonCharacters", "Character_Id");
            AddForeignKey("dbo.PlatoonCharacters", "Member_Id", "dbo.Members", "Id", cascadeDelete: true);
            AddForeignKey("dbo.PlatoonCharacters", "Character_Id", "dbo.Characters", "Id", cascadeDelete: true);
        }
    }
}
