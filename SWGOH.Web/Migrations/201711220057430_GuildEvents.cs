namespace SWGOH.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GuildEvents : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GuildEvents",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(maxLength: 255),
                        Type = c.String(maxLength: 255),
                        Alignment = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.GuildEventSchedules",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        GuildEvent_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GuildEvents", t => t.GuildEvent_Id, cascadeDelete: true)
                .Index(t => t.GuildEvent_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GuildEventSchedules", "GuildEvent_Id", "dbo.GuildEvents");
            DropIndex("dbo.GuildEventSchedules", new[] { "GuildEvent_Id" });
            DropTable("dbo.GuildEventSchedules");
            DropTable("dbo.GuildEvents");
        }
    }
}
