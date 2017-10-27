namespace SWGOH.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TerritoryPoints : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PhaseTerritories", "PointsEarned", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PhaseTerritories", "PointsEarned");
        }
    }
}
