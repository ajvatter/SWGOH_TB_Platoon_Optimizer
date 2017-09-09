namespace SWGOH.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangePlatoonModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PlatoonCharacters", "Character_Id", "dbo.Characters");
            DropForeignKey("dbo.PlatoonCharacters", "TerritoryPlatoon_Id", "dbo.TerritoryPlatoons");
            DropIndex("dbo.PlatoonCharacters", new[] { "Character_Id" });
            DropIndex("dbo.PlatoonCharacters", new[] { "TerritoryPlatoon_Id" });
            AddColumn("dbo.TerritoryPlatoons", "Character1_Id", c => c.Guid());
            AddColumn("dbo.TerritoryPlatoons", "Character2_Id", c => c.Guid());
            AddColumn("dbo.TerritoryPlatoons", "Character3_Id", c => c.Guid());
            AddColumn("dbo.TerritoryPlatoons", "Character4_Id", c => c.Guid());
            AddColumn("dbo.TerritoryPlatoons", "Character5_Id", c => c.Guid());
            AddColumn("dbo.TerritoryPlatoons", "Character6_Id", c => c.Guid());
            AddColumn("dbo.TerritoryPlatoons", "Character7_Id", c => c.Guid());
            AddColumn("dbo.TerritoryPlatoons", "Character8_Id", c => c.Guid());
            AddColumn("dbo.TerritoryPlatoons", "Character9_Id", c => c.Guid());
            AddColumn("dbo.TerritoryPlatoons", "Character10_Id", c => c.Guid());
            AddColumn("dbo.TerritoryPlatoons", "Character11_Id", c => c.Guid());
            AddColumn("dbo.TerritoryPlatoons", "Character12_Id", c => c.Guid());
            AddColumn("dbo.TerritoryPlatoons", "Character13_Id", c => c.Guid());
            AddColumn("dbo.TerritoryPlatoons", "Character14_Id", c => c.Guid());
            AddColumn("dbo.TerritoryPlatoons", "Character15_Id", c => c.Guid());
            AddColumn("dbo.TerritoryPlatoons", "Character1Member_Id", c => c.Guid());
            AddColumn("dbo.TerritoryPlatoons", "Character2Member_Id", c => c.Guid());
            AddColumn("dbo.TerritoryPlatoons", "Character3Member_Id", c => c.Guid());
            AddColumn("dbo.TerritoryPlatoons", "Character4Member_Id", c => c.Guid());
            AddColumn("dbo.TerritoryPlatoons", "Character5Member_Id", c => c.Guid());
            AddColumn("dbo.TerritoryPlatoons", "Character6Member_Id", c => c.Guid());
            AddColumn("dbo.TerritoryPlatoons", "Character7Member_Id", c => c.Guid());
            AddColumn("dbo.TerritoryPlatoons", "Character8Member_Id", c => c.Guid());
            AddColumn("dbo.TerritoryPlatoons", "Character9Member_Id", c => c.Guid());
            AddColumn("dbo.TerritoryPlatoons", "Character10Member_Id", c => c.Guid());
            AddColumn("dbo.TerritoryPlatoons", "Character11Member_Id", c => c.Guid());
            AddColumn("dbo.TerritoryPlatoons", "Character12Member_Id", c => c.Guid());
            AddColumn("dbo.TerritoryPlatoons", "Character13Member_Id", c => c.Guid());
            AddColumn("dbo.TerritoryPlatoons", "Character14Member_Id", c => c.Guid());
            AddColumn("dbo.TerritoryPlatoons", "Character15Member_Id", c => c.Guid());
            CreateIndex("dbo.TerritoryPlatoons", "Character1_Id");
            CreateIndex("dbo.TerritoryPlatoons", "Character2_Id");
            CreateIndex("dbo.TerritoryPlatoons", "Character3_Id");
            CreateIndex("dbo.TerritoryPlatoons", "Character4_Id");
            CreateIndex("dbo.TerritoryPlatoons", "Character5_Id");
            CreateIndex("dbo.TerritoryPlatoons", "Character6_Id");
            CreateIndex("dbo.TerritoryPlatoons", "Character7_Id");
            CreateIndex("dbo.TerritoryPlatoons", "Character8_Id");
            CreateIndex("dbo.TerritoryPlatoons", "Character9_Id");
            CreateIndex("dbo.TerritoryPlatoons", "Character10_Id");
            CreateIndex("dbo.TerritoryPlatoons", "Character11_Id");
            CreateIndex("dbo.TerritoryPlatoons", "Character12_Id");
            CreateIndex("dbo.TerritoryPlatoons", "Character13_Id");
            CreateIndex("dbo.TerritoryPlatoons", "Character14_Id");
            CreateIndex("dbo.TerritoryPlatoons", "Character15_Id");
            CreateIndex("dbo.TerritoryPlatoons", "Character1Member_Id");
            CreateIndex("dbo.TerritoryPlatoons", "Character2Member_Id");
            CreateIndex("dbo.TerritoryPlatoons", "Character3Member_Id");
            CreateIndex("dbo.TerritoryPlatoons", "Character4Member_Id");
            CreateIndex("dbo.TerritoryPlatoons", "Character5Member_Id");
            CreateIndex("dbo.TerritoryPlatoons", "Character6Member_Id");
            CreateIndex("dbo.TerritoryPlatoons", "Character7Member_Id");
            CreateIndex("dbo.TerritoryPlatoons", "Character8Member_Id");
            CreateIndex("dbo.TerritoryPlatoons", "Character9Member_Id");
            CreateIndex("dbo.TerritoryPlatoons", "Character10Member_Id");
            CreateIndex("dbo.TerritoryPlatoons", "Character11Member_Id");
            CreateIndex("dbo.TerritoryPlatoons", "Character12Member_Id");
            CreateIndex("dbo.TerritoryPlatoons", "Character13Member_Id");
            CreateIndex("dbo.TerritoryPlatoons", "Character14Member_Id");
            CreateIndex("dbo.TerritoryPlatoons", "Character15Member_Id");
            AddForeignKey("dbo.TerritoryPlatoons", "Character1_Id", "dbo.Characters", "Id");
            AddForeignKey("dbo.TerritoryPlatoons", "Character10_Id", "dbo.Characters", "Id");
            AddForeignKey("dbo.TerritoryPlatoons", "Character10Member_Id", "dbo.Members", "Id");
            AddForeignKey("dbo.TerritoryPlatoons", "Character11_Id", "dbo.Characters", "Id");
            AddForeignKey("dbo.TerritoryPlatoons", "Character11Member_Id", "dbo.Members", "Id");
            AddForeignKey("dbo.TerritoryPlatoons", "Character12_Id", "dbo.Characters", "Id");
            AddForeignKey("dbo.TerritoryPlatoons", "Character12Member_Id", "dbo.Members", "Id");
            AddForeignKey("dbo.TerritoryPlatoons", "Character13_Id", "dbo.Characters", "Id");
            AddForeignKey("dbo.TerritoryPlatoons", "Character13Member_Id", "dbo.Members", "Id");
            AddForeignKey("dbo.TerritoryPlatoons", "Character14_Id", "dbo.Characters", "Id");
            AddForeignKey("dbo.TerritoryPlatoons", "Character14Member_Id", "dbo.Members", "Id");
            AddForeignKey("dbo.TerritoryPlatoons", "Character15_Id", "dbo.Characters", "Id");
            AddForeignKey("dbo.TerritoryPlatoons", "Character15Member_Id", "dbo.Members", "Id");
            AddForeignKey("dbo.TerritoryPlatoons", "Character1Member_Id", "dbo.Members", "Id");
            AddForeignKey("dbo.TerritoryPlatoons", "Character2_Id", "dbo.Characters", "Id");
            AddForeignKey("dbo.TerritoryPlatoons", "Character2Member_Id", "dbo.Members", "Id");
            AddForeignKey("dbo.TerritoryPlatoons", "Character3_Id", "dbo.Characters", "Id");
            AddForeignKey("dbo.TerritoryPlatoons", "Character3Member_Id", "dbo.Members", "Id");
            AddForeignKey("dbo.TerritoryPlatoons", "Character4_Id", "dbo.Characters", "Id");
            AddForeignKey("dbo.TerritoryPlatoons", "Character4Member_Id", "dbo.Members", "Id");
            AddForeignKey("dbo.TerritoryPlatoons", "Character5_Id", "dbo.Characters", "Id");
            AddForeignKey("dbo.TerritoryPlatoons", "Character5Member_Id", "dbo.Members", "Id");
            AddForeignKey("dbo.TerritoryPlatoons", "Character6_Id", "dbo.Characters", "Id");
            AddForeignKey("dbo.TerritoryPlatoons", "Character6Member_Id", "dbo.Members", "Id");
            AddForeignKey("dbo.TerritoryPlatoons", "Character7_Id", "dbo.Characters", "Id");
            AddForeignKey("dbo.TerritoryPlatoons", "Character7Member_Id", "dbo.Members", "Id");
            AddForeignKey("dbo.TerritoryPlatoons", "Character8_Id", "dbo.Characters", "Id");
            AddForeignKey("dbo.TerritoryPlatoons", "Character8Member_Id", "dbo.Members", "Id");
            AddForeignKey("dbo.TerritoryPlatoons", "Character9_Id", "dbo.Characters", "Id");
            AddForeignKey("dbo.TerritoryPlatoons", "Character9Member_Id", "dbo.Members", "Id");
            DropTable("dbo.PlatoonCharacters");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.PlatoonCharacters",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Character_Id = c.Guid(nullable: false),
                        QtyRequired = c.Int(nullable: false),
                        TerritoryPlatoon_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.TerritoryPlatoons", "Character9Member_Id", "dbo.Members");
            DropForeignKey("dbo.TerritoryPlatoons", "Character9_Id", "dbo.Characters");
            DropForeignKey("dbo.TerritoryPlatoons", "Character8Member_Id", "dbo.Members");
            DropForeignKey("dbo.TerritoryPlatoons", "Character8_Id", "dbo.Characters");
            DropForeignKey("dbo.TerritoryPlatoons", "Character7Member_Id", "dbo.Members");
            DropForeignKey("dbo.TerritoryPlatoons", "Character7_Id", "dbo.Characters");
            DropForeignKey("dbo.TerritoryPlatoons", "Character6Member_Id", "dbo.Members");
            DropForeignKey("dbo.TerritoryPlatoons", "Character6_Id", "dbo.Characters");
            DropForeignKey("dbo.TerritoryPlatoons", "Character5Member_Id", "dbo.Members");
            DropForeignKey("dbo.TerritoryPlatoons", "Character5_Id", "dbo.Characters");
            DropForeignKey("dbo.TerritoryPlatoons", "Character4Member_Id", "dbo.Members");
            DropForeignKey("dbo.TerritoryPlatoons", "Character4_Id", "dbo.Characters");
            DropForeignKey("dbo.TerritoryPlatoons", "Character3Member_Id", "dbo.Members");
            DropForeignKey("dbo.TerritoryPlatoons", "Character3_Id", "dbo.Characters");
            DropForeignKey("dbo.TerritoryPlatoons", "Character2Member_Id", "dbo.Members");
            DropForeignKey("dbo.TerritoryPlatoons", "Character2_Id", "dbo.Characters");
            DropForeignKey("dbo.TerritoryPlatoons", "Character1Member_Id", "dbo.Members");
            DropForeignKey("dbo.TerritoryPlatoons", "Character15Member_Id", "dbo.Members");
            DropForeignKey("dbo.TerritoryPlatoons", "Character15_Id", "dbo.Characters");
            DropForeignKey("dbo.TerritoryPlatoons", "Character14Member_Id", "dbo.Members");
            DropForeignKey("dbo.TerritoryPlatoons", "Character14_Id", "dbo.Characters");
            DropForeignKey("dbo.TerritoryPlatoons", "Character13Member_Id", "dbo.Members");
            DropForeignKey("dbo.TerritoryPlatoons", "Character13_Id", "dbo.Characters");
            DropForeignKey("dbo.TerritoryPlatoons", "Character12Member_Id", "dbo.Members");
            DropForeignKey("dbo.TerritoryPlatoons", "Character12_Id", "dbo.Characters");
            DropForeignKey("dbo.TerritoryPlatoons", "Character11Member_Id", "dbo.Members");
            DropForeignKey("dbo.TerritoryPlatoons", "Character11_Id", "dbo.Characters");
            DropForeignKey("dbo.TerritoryPlatoons", "Character10Member_Id", "dbo.Members");
            DropForeignKey("dbo.TerritoryPlatoons", "Character10_Id", "dbo.Characters");
            DropForeignKey("dbo.TerritoryPlatoons", "Character1_Id", "dbo.Characters");
            DropIndex("dbo.TerritoryPlatoons", new[] { "Character15Member_Id" });
            DropIndex("dbo.TerritoryPlatoons", new[] { "Character14Member_Id" });
            DropIndex("dbo.TerritoryPlatoons", new[] { "Character13Member_Id" });
            DropIndex("dbo.TerritoryPlatoons", new[] { "Character12Member_Id" });
            DropIndex("dbo.TerritoryPlatoons", new[] { "Character11Member_Id" });
            DropIndex("dbo.TerritoryPlatoons", new[] { "Character10Member_Id" });
            DropIndex("dbo.TerritoryPlatoons", new[] { "Character9Member_Id" });
            DropIndex("dbo.TerritoryPlatoons", new[] { "Character8Member_Id" });
            DropIndex("dbo.TerritoryPlatoons", new[] { "Character7Member_Id" });
            DropIndex("dbo.TerritoryPlatoons", new[] { "Character6Member_Id" });
            DropIndex("dbo.TerritoryPlatoons", new[] { "Character5Member_Id" });
            DropIndex("dbo.TerritoryPlatoons", new[] { "Character4Member_Id" });
            DropIndex("dbo.TerritoryPlatoons", new[] { "Character3Member_Id" });
            DropIndex("dbo.TerritoryPlatoons", new[] { "Character2Member_Id" });
            DropIndex("dbo.TerritoryPlatoons", new[] { "Character1Member_Id" });
            DropIndex("dbo.TerritoryPlatoons", new[] { "Character15_Id" });
            DropIndex("dbo.TerritoryPlatoons", new[] { "Character14_Id" });
            DropIndex("dbo.TerritoryPlatoons", new[] { "Character13_Id" });
            DropIndex("dbo.TerritoryPlatoons", new[] { "Character12_Id" });
            DropIndex("dbo.TerritoryPlatoons", new[] { "Character11_Id" });
            DropIndex("dbo.TerritoryPlatoons", new[] { "Character10_Id" });
            DropIndex("dbo.TerritoryPlatoons", new[] { "Character9_Id" });
            DropIndex("dbo.TerritoryPlatoons", new[] { "Character8_Id" });
            DropIndex("dbo.TerritoryPlatoons", new[] { "Character7_Id" });
            DropIndex("dbo.TerritoryPlatoons", new[] { "Character6_Id" });
            DropIndex("dbo.TerritoryPlatoons", new[] { "Character5_Id" });
            DropIndex("dbo.TerritoryPlatoons", new[] { "Character4_Id" });
            DropIndex("dbo.TerritoryPlatoons", new[] { "Character3_Id" });
            DropIndex("dbo.TerritoryPlatoons", new[] { "Character2_Id" });
            DropIndex("dbo.TerritoryPlatoons", new[] { "Character1_Id" });
            DropColumn("dbo.TerritoryPlatoons", "Character15Member_Id");
            DropColumn("dbo.TerritoryPlatoons", "Character14Member_Id");
            DropColumn("dbo.TerritoryPlatoons", "Character13Member_Id");
            DropColumn("dbo.TerritoryPlatoons", "Character12Member_Id");
            DropColumn("dbo.TerritoryPlatoons", "Character11Member_Id");
            DropColumn("dbo.TerritoryPlatoons", "Character10Member_Id");
            DropColumn("dbo.TerritoryPlatoons", "Character9Member_Id");
            DropColumn("dbo.TerritoryPlatoons", "Character8Member_Id");
            DropColumn("dbo.TerritoryPlatoons", "Character7Member_Id");
            DropColumn("dbo.TerritoryPlatoons", "Character6Member_Id");
            DropColumn("dbo.TerritoryPlatoons", "Character5Member_Id");
            DropColumn("dbo.TerritoryPlatoons", "Character4Member_Id");
            DropColumn("dbo.TerritoryPlatoons", "Character3Member_Id");
            DropColumn("dbo.TerritoryPlatoons", "Character2Member_Id");
            DropColumn("dbo.TerritoryPlatoons", "Character1Member_Id");
            DropColumn("dbo.TerritoryPlatoons", "Character15_Id");
            DropColumn("dbo.TerritoryPlatoons", "Character14_Id");
            DropColumn("dbo.TerritoryPlatoons", "Character13_Id");
            DropColumn("dbo.TerritoryPlatoons", "Character12_Id");
            DropColumn("dbo.TerritoryPlatoons", "Character11_Id");
            DropColumn("dbo.TerritoryPlatoons", "Character10_Id");
            DropColumn("dbo.TerritoryPlatoons", "Character9_Id");
            DropColumn("dbo.TerritoryPlatoons", "Character8_Id");
            DropColumn("dbo.TerritoryPlatoons", "Character7_Id");
            DropColumn("dbo.TerritoryPlatoons", "Character6_Id");
            DropColumn("dbo.TerritoryPlatoons", "Character5_Id");
            DropColumn("dbo.TerritoryPlatoons", "Character4_Id");
            DropColumn("dbo.TerritoryPlatoons", "Character3_Id");
            DropColumn("dbo.TerritoryPlatoons", "Character2_Id");
            DropColumn("dbo.TerritoryPlatoons", "Character1_Id");
            CreateIndex("dbo.PlatoonCharacters", "TerritoryPlatoon_Id");
            CreateIndex("dbo.PlatoonCharacters", "Character_Id");
            AddForeignKey("dbo.PlatoonCharacters", "TerritoryPlatoon_Id", "dbo.TerritoryPlatoons", "Id");
            AddForeignKey("dbo.PlatoonCharacters", "Character_Id", "dbo.Characters", "Id", cascadeDelete: true);
        }
    }
}
