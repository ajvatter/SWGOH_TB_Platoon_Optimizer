namespace SWGOH.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EvenMoreChanges : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TerritoryPlatoons", "TerritoryBattle_Id", "dbo.TerritoryBattles");
            DropIndex("dbo.TerritoryPlatoons", new[] { "TerritoryBattle_Id" });
            DropColumn("dbo.TerritoryPlatoons", "TerritoryBattle_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TerritoryPlatoons", "TerritoryBattle_Id", c => c.Guid());
            CreateIndex("dbo.TerritoryPlatoons", "TerritoryBattle_Id");
            AddForeignKey("dbo.TerritoryPlatoons", "TerritoryBattle_Id", "dbo.TerritoryBattles", "Id");
        }
    }
}
