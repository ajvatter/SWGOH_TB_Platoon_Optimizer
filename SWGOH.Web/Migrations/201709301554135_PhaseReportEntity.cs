namespace SWGOH.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PhaseReportEntity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PhaseReports",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TerritoryBattlePhase_Id = c.Guid(nullable: false),
                        MemberCharacter_Id = c.Guid(),
                        MemberShip_Id = c.Guid(),
                        PlatoonCharacter_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MemberCharacters", t => t.MemberCharacter_Id)
                .ForeignKey("dbo.PlatoonCharacters", t => t.PlatoonCharacter_Id)
                .ForeignKey("dbo.TerritoryBattlePhases", t => t.TerritoryBattlePhase_Id, cascadeDelete: true)
                .Index(t => t.TerritoryBattlePhase_Id)
                .Index(t => t.MemberCharacter_Id)
                .Index(t => t.PlatoonCharacter_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PhaseReports", "TerritoryBattlePhase_Id", "dbo.TerritoryBattlePhases");
            DropForeignKey("dbo.PhaseReports", "PlatoonCharacter_Id", "dbo.PlatoonCharacters");
            DropForeignKey("dbo.PhaseReports", "MemberCharacter_Id", "dbo.MemberCharacters");
            DropIndex("dbo.PhaseReports", new[] { "PlatoonCharacter_Id" });
            DropIndex("dbo.PhaseReports", new[] { "MemberCharacter_Id" });
            DropIndex("dbo.PhaseReports", new[] { "TerritoryBattlePhase_Id" });
            DropTable("dbo.PhaseReports");
        }
    }
}
