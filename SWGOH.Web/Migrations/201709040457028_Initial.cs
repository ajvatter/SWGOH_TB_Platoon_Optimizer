namespace SWGOH.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Characters",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(maxLength: 255),
                        UrlExt = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Guilds",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(maxLength: 255),
                        UrlExt = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Members",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(maxLength: 255),
                        UrlExt = c.String(maxLength: 255),
                        Guild_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Guilds", t => t.Guild_Id, cascadeDelete: true)
                .Index(t => t.Guild_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Members", "Guild_Id", "dbo.Guilds");
            DropIndex("dbo.Members", new[] { "Guild_Id" });
            DropTable("dbo.Members");
            DropTable("dbo.Guilds");
            DropTable("dbo.Characters");
        }
    }
}
