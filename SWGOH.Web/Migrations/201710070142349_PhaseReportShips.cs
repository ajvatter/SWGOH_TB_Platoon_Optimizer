namespace SWGOH.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PhaseReportShips : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PhaseReports", "PlatoonShip_Id", c => c.Guid());
            CreateIndex("dbo.PhaseReports", "PlatoonShip_Id");
            AddForeignKey("dbo.PhaseReports", "PlatoonShip_Id", "dbo.PlatoonShips", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PhaseReports", "PlatoonShip_Id", "dbo.PlatoonShips");
            DropIndex("dbo.PhaseReports", new[] { "PlatoonShip_Id" });
            DropColumn("dbo.PhaseReports", "PlatoonShip_Id");
        }
    }
}
