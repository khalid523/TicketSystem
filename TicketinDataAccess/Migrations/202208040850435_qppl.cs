namespace TicketinDataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class qppl : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Histories", "ProjectsId", c => c.Int());
            AddColumn("dbo.Histories", "Description", c => c.String());
            CreateIndex("dbo.Histories", "ProjectsId");
            AddForeignKey("dbo.Histories", "ProjectsId", "dbo.Projects", "Id");
            DropColumn("dbo.Tickets", "isLeader");
            DropColumn("dbo.Histories", "Action");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Histories", "Action", c => c.String());
            AddColumn("dbo.Tickets", "isLeader", c => c.Boolean(nullable: false));
            DropForeignKey("dbo.Histories", "ProjectsId", "dbo.Projects");
            DropIndex("dbo.Histories", new[] { "ProjectsId" });
            DropColumn("dbo.Histories", "Description");
            DropColumn("dbo.Histories", "ProjectsId");
        }
    }
}
