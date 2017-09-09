namespace SWGOH.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TerritoryBattleModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PhaseTerritories",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TotalPointsEarned = c.Int(),
                        TerritoryPlatoon1_Id = c.Guid(),
                        TerritoryPlatoon2_Id = c.Guid(),
                        TerritoryPlatoon3_Id = c.Guid(),
                        TerritoryPlatoon4_Id = c.Guid(),
                        TerritoryPlatoon5_Id = c.Guid(),
                        TerritoryPlatoon6_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TerritoryPlatoons", t => t.TerritoryPlatoon1_Id)
                .ForeignKey("dbo.TerritoryPlatoons", t => t.TerritoryPlatoon2_Id)
                .ForeignKey("dbo.TerritoryPlatoons", t => t.TerritoryPlatoon3_Id)
                .ForeignKey("dbo.TerritoryPlatoons", t => t.TerritoryPlatoon4_Id)
                .ForeignKey("dbo.TerritoryPlatoons", t => t.TerritoryPlatoon5_Id)
                .ForeignKey("dbo.TerritoryPlatoons", t => t.TerritoryPlatoon6_Id)
                .Index(t => t.TerritoryPlatoon1_Id)
                .Index(t => t.TerritoryPlatoon2_Id)
                .Index(t => t.TerritoryPlatoon3_Id)
                .Index(t => t.TerritoryPlatoon4_Id)
                .Index(t => t.TerritoryPlatoon5_Id)
                .Index(t => t.TerritoryPlatoon6_Id);
            
            CreateTable(
                "dbo.TerritoryPlatoons",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PlatoonCharacters",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Character_Id = c.Guid(nullable: false),
                        QtyRequired = c.Int(nullable: false),
                        TerritoryPlatoon_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Characters", t => t.Character_Id, cascadeDelete: true)
                .ForeignKey("dbo.TerritoryPlatoons", t => t.TerritoryPlatoon_Id)
                .Index(t => t.Character_Id)
                .Index(t => t.TerritoryPlatoon_Id);
            
            CreateTable(
                "dbo.TerritoryBattlePhases",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        RequiredStars = c.Int(nullable: false),
                        Territory1_Id = c.Guid(),
                        HasSecondTerritory = c.Boolean(nullable: false),
                        Territory2_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PhaseTerritories", t => t.Territory1_Id)
                .ForeignKey("dbo.PhaseTerritories", t => t.Territory2_Id)
                .Index(t => t.Territory1_Id)
                .Index(t => t.Territory2_Id);
            
            CreateTable(
                "dbo.TerritoryBattles",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Guild_Id = c.Guid(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        TotalStars = c.Int(),
                        IsActive = c.Boolean(nullable: false),
                        Phase1_Id = c.Guid(),
                        Phase2_Id = c.Guid(),
                        Phase3_Id = c.Guid(),
                        Phase4_Id = c.Guid(),
                        Phase5_Id = c.Guid(),
                        Phase6_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Guilds", t => t.Guild_Id, cascadeDelete: true)
                .ForeignKey("dbo.TerritoryBattlePhases", t => t.Phase1_Id)
                .ForeignKey("dbo.TerritoryBattlePhases", t => t.Phase2_Id)
                .ForeignKey("dbo.TerritoryBattlePhases", t => t.Phase3_Id)
                .ForeignKey("dbo.TerritoryBattlePhases", t => t.Phase4_Id)
                .ForeignKey("dbo.TerritoryBattlePhases", t => t.Phase5_Id)
                .ForeignKey("dbo.TerritoryBattlePhases", t => t.Phase6_Id)
                .Index(t => t.Guild_Id)
                .Index(t => t.Phase1_Id)
                .Index(t => t.Phase2_Id)
                .Index(t => t.Phase3_Id)
                .Index(t => t.Phase4_Id)
                .Index(t => t.Phase5_Id)
                .Index(t => t.Phase6_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TerritoryBattles", "Phase6_Id", "dbo.TerritoryBattlePhases");
            DropForeignKey("dbo.TerritoryBattles", "Phase5_Id", "dbo.TerritoryBattlePhases");
            DropForeignKey("dbo.TerritoryBattles", "Phase4_Id", "dbo.TerritoryBattlePhases");
            DropForeignKey("dbo.TerritoryBattles", "Phase3_Id", "dbo.TerritoryBattlePhases");
            DropForeignKey("dbo.TerritoryBattles", "Phase2_Id", "dbo.TerritoryBattlePhases");
            DropForeignKey("dbo.TerritoryBattles", "Phase1_Id", "dbo.TerritoryBattlePhases");
            DropForeignKey("dbo.TerritoryBattles", "Guild_Id", "dbo.Guilds");
            DropForeignKey("dbo.TerritoryBattlePhases", "Territory2_Id", "dbo.PhaseTerritories");
            DropForeignKey("dbo.TerritoryBattlePhases", "Territory1_Id", "dbo.PhaseTerritories");
            DropForeignKey("dbo.PhaseTerritories", "TerritoryPlatoon6_Id", "dbo.TerritoryPlatoons");
            DropForeignKey("dbo.PhaseTerritories", "TerritoryPlatoon5_Id", "dbo.TerritoryPlatoons");
            DropForeignKey("dbo.PhaseTerritories", "TerritoryPlatoon4_Id", "dbo.TerritoryPlatoons");
            DropForeignKey("dbo.PhaseTerritories", "TerritoryPlatoon3_Id", "dbo.TerritoryPlatoons");
            DropForeignKey("dbo.PhaseTerritories", "TerritoryPlatoon2_Id", "dbo.TerritoryPlatoons");
            DropForeignKey("dbo.PhaseTerritories", "TerritoryPlatoon1_Id", "dbo.TerritoryPlatoons");
            DropForeignKey("dbo.PlatoonCharacters", "TerritoryPlatoon_Id", "dbo.TerritoryPlatoons");
            DropForeignKey("dbo.PlatoonCharacters", "Character_Id", "dbo.Characters");
            DropIndex("dbo.TerritoryBattles", new[] { "Phase6_Id" });
            DropIndex("dbo.TerritoryBattles", new[] { "Phase5_Id" });
            DropIndex("dbo.TerritoryBattles", new[] { "Phase4_Id" });
            DropIndex("dbo.TerritoryBattles", new[] { "Phase3_Id" });
            DropIndex("dbo.TerritoryBattles", new[] { "Phase2_Id" });
            DropIndex("dbo.TerritoryBattles", new[] { "Phase1_Id" });
            DropIndex("dbo.TerritoryBattles", new[] { "Guild_Id" });
            DropIndex("dbo.TerritoryBattlePhases", new[] { "Territory2_Id" });
            DropIndex("dbo.TerritoryBattlePhases", new[] { "Territory1_Id" });
            DropIndex("dbo.PlatoonCharacters", new[] { "TerritoryPlatoon_Id" });
            DropIndex("dbo.PlatoonCharacters", new[] { "Character_Id" });
            DropIndex("dbo.PhaseTerritories", new[] { "TerritoryPlatoon6_Id" });
            DropIndex("dbo.PhaseTerritories", new[] { "TerritoryPlatoon5_Id" });
            DropIndex("dbo.PhaseTerritories", new[] { "TerritoryPlatoon4_Id" });
            DropIndex("dbo.PhaseTerritories", new[] { "TerritoryPlatoon3_Id" });
            DropIndex("dbo.PhaseTerritories", new[] { "TerritoryPlatoon2_Id" });
            DropIndex("dbo.PhaseTerritories", new[] { "TerritoryPlatoon1_Id" });
            DropTable("dbo.TerritoryBattles");
            DropTable("dbo.TerritoryBattlePhases");
            DropTable("dbo.PlatoonCharacters");
            DropTable("dbo.TerritoryPlatoons");
            DropTable("dbo.PhaseTerritories");
        }
    }
}
