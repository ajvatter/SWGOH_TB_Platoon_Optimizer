namespace SWGOH.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changes : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PhaseTerritories", "TerritoryBattle_Id", "dbo.TerritoryBattles");
            DropForeignKey("dbo.TerritoryBattlePhases", "TerritoryBattle_Id", "dbo.TerritoryBattles");
            DropForeignKey("dbo.TerritoryPlatoons", "TerritoryBattle_Id", "dbo.TerritoryBattles");
            DropIndex("dbo.PhaseTerritories", new[] { "TerritoryBattle_Id" });
            DropIndex("dbo.TerritoryBattlePhases", new[] { "TerritoryBattle_Id" });
            DropIndex("dbo.TerritoryPlatoons", new[] { "TerritoryBattle_Id" });
            AlterColumn("dbo.PhaseTerritories", "TerritoryBattle_Id", c => c.Guid());
            AlterColumn("dbo.TerritoryBattlePhases", "TerritoryBattle_Id", c => c.Guid());
            AlterColumn("dbo.TerritoryPlatoons", "TerritoryBattle_Id", c => c.Guid());
            CreateIndex("dbo.PhaseTerritories", "TerritoryBattle_Id");
            CreateIndex("dbo.TerritoryBattlePhases", "TerritoryBattle_Id");
            CreateIndex("dbo.TerritoryPlatoons", "TerritoryBattle_Id");
            AddForeignKey("dbo.PhaseTerritories", "TerritoryBattle_Id", "dbo.TerritoryBattles", "Id");
            AddForeignKey("dbo.TerritoryBattlePhases", "TerritoryBattle_Id", "dbo.TerritoryBattles", "Id");
            AddForeignKey("dbo.TerritoryPlatoons", "TerritoryBattle_Id", "dbo.TerritoryBattles", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TerritoryPlatoons", "TerritoryBattle_Id", "dbo.TerritoryBattles");
            DropForeignKey("dbo.TerritoryBattlePhases", "TerritoryBattle_Id", "dbo.TerritoryBattles");
            DropForeignKey("dbo.PhaseTerritories", "TerritoryBattle_Id", "dbo.TerritoryBattles");
            DropIndex("dbo.TerritoryPlatoons", new[] { "TerritoryBattle_Id" });
            DropIndex("dbo.TerritoryBattlePhases", new[] { "TerritoryBattle_Id" });
            DropIndex("dbo.PhaseTerritories", new[] { "TerritoryBattle_Id" });
            AlterColumn("dbo.TerritoryPlatoons", "TerritoryBattle_Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.TerritoryBattlePhases", "TerritoryBattle_Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.PhaseTerritories", "TerritoryBattle_Id", c => c.Guid(nullable: false));
            CreateIndex("dbo.TerritoryPlatoons", "TerritoryBattle_Id");
            CreateIndex("dbo.TerritoryBattlePhases", "TerritoryBattle_Id");
            CreateIndex("dbo.PhaseTerritories", "TerritoryBattle_Id");
            AddForeignKey("dbo.TerritoryPlatoons", "TerritoryBattle_Id", "dbo.TerritoryBattles", "Id", cascadeDelete: true);
            AddForeignKey("dbo.TerritoryBattlePhases", "TerritoryBattle_Id", "dbo.TerritoryBattles", "Id", cascadeDelete: true);
            AddForeignKey("dbo.PhaseTerritories", "TerritoryBattle_Id", "dbo.TerritoryBattles", "Id", cascadeDelete: true);
        }
    }
}
