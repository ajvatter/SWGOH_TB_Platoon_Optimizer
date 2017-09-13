namespace SWGOH.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MoreChanges : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TerritoryBattlePhases", "TerritoryBattle_Id", "dbo.TerritoryBattles");
            DropForeignKey("dbo.PhaseTerritories", "TerritoryBattle_Id", "dbo.TerritoryBattles");
            DropIndex("dbo.PhaseTerritories", new[] { "TerritoryBattle_Id" });
            DropIndex("dbo.TerritoryBattlePhases", new[] { "TerritoryBattle_Id" });
            DropColumn("dbo.PhaseTerritories", "TerritoryBattle_Id");
            DropColumn("dbo.TerritoryBattlePhases", "TerritoryBattle_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TerritoryBattlePhases", "TerritoryBattle_Id", c => c.Guid());
            AddColumn("dbo.PhaseTerritories", "TerritoryBattle_Id", c => c.Guid());
            CreateIndex("dbo.TerritoryBattlePhases", "TerritoryBattle_Id");
            CreateIndex("dbo.PhaseTerritories", "TerritoryBattle_Id");
            AddForeignKey("dbo.PhaseTerritories", "TerritoryBattle_Id", "dbo.TerritoryBattles", "Id");
            AddForeignKey("dbo.TerritoryBattlePhases", "TerritoryBattle_Id", "dbo.TerritoryBattles", "Id");
        }
    }
}
