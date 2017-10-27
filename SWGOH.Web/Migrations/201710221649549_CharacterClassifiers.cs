namespace SWGOH.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CharacterClassifiers : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CharacterClassifiers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CharacterClassifierCharacters",
                c => new
                    {
                        CharacterClassifier_Id = c.Guid(nullable: false),
                        Character_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.CharacterClassifier_Id, t.Character_Id })
                .ForeignKey("dbo.CharacterClassifiers", t => t.CharacterClassifier_Id, cascadeDelete: true)
                .ForeignKey("dbo.Characters", t => t.Character_Id, cascadeDelete: true)
                .Index(t => t.CharacterClassifier_Id)
                .Index(t => t.Character_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CharacterClassifierCharacters", "Character_Id", "dbo.Characters");
            DropForeignKey("dbo.CharacterClassifierCharacters", "CharacterClassifier_Id", "dbo.CharacterClassifiers");
            DropIndex("dbo.CharacterClassifierCharacters", new[] { "Character_Id" });
            DropIndex("dbo.CharacterClassifierCharacters", new[] { "CharacterClassifier_Id" });
            DropTable("dbo.CharacterClassifierCharacters");
            DropTable("dbo.CharacterClassifiers");
        }
    }
}
