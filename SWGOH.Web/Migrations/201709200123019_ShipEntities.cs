namespace SWGOH.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ShipEntities : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MemberShips",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Level = c.Int(nullable: false),
                        Stars = c.Int(),
                        Power = c.String(),
                        Ship_Id = c.Guid(nullable: false),
                        Member_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Members", t => t.Member_Id, cascadeDelete: true)
                .ForeignKey("dbo.Ships", t => t.Ship_Id, cascadeDelete: true)
                .Index(t => t.Ship_Id)
                .Index(t => t.Member_Id);
            
            CreateTable(
                "dbo.Ships",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(maxLength: 255),
                        DisplayName = c.String(maxLength: 255),
                        UrlExt = c.String(maxLength: 255),
                        Alignment = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MemberShips", "Ship_Id", "dbo.Ships");
            DropForeignKey("dbo.MemberShips", "Member_Id", "dbo.Members");
            DropIndex("dbo.MemberShips", new[] { "Member_Id" });
            DropIndex("dbo.MemberShips", new[] { "Ship_Id" });
            DropTable("dbo.Ships");
            DropTable("dbo.MemberShips");
        }
    }
}
