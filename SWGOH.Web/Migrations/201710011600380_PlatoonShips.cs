namespace SWGOH.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PlatoonShips : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PlatoonCharacters", "Member_Id", "dbo.Members");
            DropIndex("dbo.PlatoonCharacters", new[] { "Member_Id" });
            CreateTable(
                "dbo.PlatoonShips",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        PlatoonPosition = c.Int(nullable: false),
                        Ship_Id = c.Guid(),
                        TerritoryPlatoon_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Ships", t => t.Ship_Id)
                .ForeignKey("dbo.TerritoryPlatoons", t => t.TerritoryPlatoon_Id, cascadeDelete: true)
                .Index(t => t.Ship_Id)
                .Index(t => t.TerritoryPlatoon_Id);
            
            AddColumn("dbo.TerritoryBattlePhases", "HasThirdTerritory", c => c.Boolean(nullable: false));
            DropColumn("dbo.PlatoonCharacters", "Member_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PlatoonCharacters", "Member_Id", c => c.Guid());
            DropForeignKey("dbo.PlatoonShips", "TerritoryPlatoon_Id", "dbo.TerritoryPlatoons");
            DropForeignKey("dbo.PlatoonShips", "Ship_Id", "dbo.Ships");
            DropIndex("dbo.PlatoonShips", new[] { "TerritoryPlatoon_Id" });
            DropIndex("dbo.PlatoonShips", new[] { "Ship_Id" });
            DropColumn("dbo.TerritoryBattlePhases", "HasThirdTerritory");
            DropTable("dbo.PlatoonShips");
            CreateIndex("dbo.PlatoonCharacters", "Member_Id");
            AddForeignKey("dbo.PlatoonCharacters", "Member_Id", "dbo.Members", "Id");
        }
    }
}
