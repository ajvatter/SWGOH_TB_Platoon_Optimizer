namespace SWGOH.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PowerToInt : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.MemberCharacters", "Power", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MemberCharacters", "Power", c => c.String());
        }
    }
}
