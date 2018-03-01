namespace SWGOH.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RefreshReportParameter : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TerritoryBattlePhases", "RefreshReport", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TerritoryBattlePhases", "RefreshReport");
        }
    }
}
