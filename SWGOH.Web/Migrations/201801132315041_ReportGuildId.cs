namespace SWGOH.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReportGuildId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PhaseReports", "GuildId", c => c.Guid());
            CreateIndex("dbo.PhaseReports", "GuildId");
            AddForeignKey("dbo.PhaseReports", "GuildId", "dbo.Guilds", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PhaseReports", "GuildId", "dbo.Guilds");
            DropIndex("dbo.PhaseReports", new[] { "GuildId" });
            DropColumn("dbo.PhaseReports", "GuildId");
        }
    }
}
