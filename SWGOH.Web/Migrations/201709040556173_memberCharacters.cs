namespace SWGOH.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class memberCharacters : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MemberCharacters",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Level = c.Int(nullable: false),
                        Gear = c.String(),
                        Stars = c.Int(),
                        Power = c.String(),
                        Character_Id = c.Guid(nullable: false),
                        Member_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Characters", t => t.Character_Id, cascadeDelete: true)
                .ForeignKey("dbo.Members", t => t.Member_Id, cascadeDelete: true)
                .Index(t => t.Character_Id)
                .Index(t => t.Member_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MemberCharacters", "Member_Id", "dbo.Members");
            DropForeignKey("dbo.MemberCharacters", "Character_Id", "dbo.Characters");
            DropIndex("dbo.MemberCharacters", new[] { "Member_Id" });
            DropIndex("dbo.MemberCharacters", new[] { "Character_Id" });
            DropTable("dbo.MemberCharacters");
        }
    }
}
