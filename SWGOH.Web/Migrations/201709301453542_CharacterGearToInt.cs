namespace SWGOH.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CharacterGearToInt : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.MemberCharacters", "Gear", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MemberCharacters", "Gear", c => c.String());
        }
    }
}
