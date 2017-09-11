namespace SWGOH.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTbRelation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PhaseTerritories", "TerritoryBattle_Id", c => c.Guid(nullable: false));
            AddColumn("dbo.TerritoryPlatoons", "TerritoryBattle_Id", c => c.Guid(nullable: false));
            AddColumn("dbo.TerritoryBattlePhases", "TerritoryBattle_Id", c => c.Guid(nullable: false));
            CreateIndex("dbo.PhaseTerritories", "TerritoryBattle_Id");
            CreateIndex("dbo.TerritoryBattlePhases", "TerritoryBattle_Id");
            CreateIndex("dbo.TerritoryPlatoons", "TerritoryBattle_Id");
            AddForeignKey("dbo.TerritoryBattlePhases", "TerritoryBattle_Id", "dbo.TerritoryBattles", "Id", cascadeDelete: true);
            AddForeignKey("dbo.PhaseTerritories", "TerritoryBattle_Id", "dbo.TerritoryBattles", "Id", cascadeDelete: true);
            AddForeignKey("dbo.TerritoryPlatoons", "TerritoryBattle_Id", "dbo.TerritoryBattles", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TerritoryPlatoons", "TerritoryBattle_Id", "dbo.TerritoryBattles");
            DropForeignKey("dbo.PhaseTerritories", "TerritoryBattle_Id", "dbo.TerritoryBattles");
            DropForeignKey("dbo.TerritoryBattlePhases", "TerritoryBattle_Id", "dbo.TerritoryBattles");
            DropIndex("dbo.TerritoryPlatoons", new[] { "TerritoryBattle_Id" });
            DropIndex("dbo.TerritoryBattlePhases", new[] { "TerritoryBattle_Id" });
            DropIndex("dbo.PhaseTerritories", new[] { "TerritoryBattle_Id" });
            DropColumn("dbo.TerritoryBattlePhases", "TerritoryBattle_Id");
            DropColumn("dbo.TerritoryPlatoons", "TerritoryBattle_Id");
            DropColumn("dbo.PhaseTerritories", "TerritoryBattle_Id");
        }
    }
}
