namespace SWGOH.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PhaseReportsShips2 : DbMigration
    {
        public override void Up()
        {
            //DropColumn("dbo.PhaseReports", "MemberShip_Id");
            //RenameColumn(table: "dbo.PhaseReports", name: "MemberCharacter_Id", newName: "MemberShip_Id");
            //RenameIndex(table: "dbo.PhaseReports", name: "IX_MemberCharacter_Id", newName: "IX_MemberShip_Id");
            AddForeignKey("dbo.PhaseReports", "MemberShip_Id", "dbo.MemberShips", "Id");
            CreateIndex("dbo.PhaseReports", "MemberShip_Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.PhaseReports", new[] { "MemberShip_Id" });
            AddForeignKey("dbo.PhaseReports", "MemberShip_Id", "dbo.MemberShips");
            // RenameIndex(table: "dbo.PhaseReports", name: "IX_MemberShip_Id", newName: "IX_MemberCharacter_Id");
            //RenameColumn(table: "dbo.PhaseReports", name: "MemberShip_Id", newName: "MemberCharacter_Id");
            //AddColumn("dbo.PhaseReports", "MemberShip_Id", c => c.Guid());
        }
    }
}
