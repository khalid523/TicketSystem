namespace TicketinDataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class asdfcvxx : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProjectMembers", "Step", c => c.String());
            AlterColumn("dbo.Tickets", "Status", c => c.String());
            DropColumn("dbo.Tickets", "DonePa");
            DropColumn("dbo.Tickets", "RejectPa");
            DropColumn("dbo.Tickets", "DoneDev");
            DropColumn("dbo.Tickets", "PullPa");
            DropColumn("dbo.Tickets", "PullDev");
            DropColumn("dbo.ProjectMembers", "DonePa");
            DropColumn("dbo.ProjectMembers", "RejectPa");
            DropColumn("dbo.ProjectMembers", "DoneDev");
            DropColumn("dbo.ProjectMembers", "PullPa");
            DropColumn("dbo.ProjectMembers", "PullDev");
            DropColumn("dbo.Histories", "esclation");
            DropTable("dbo.Audits");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Audits",
                c => new
                    {
                        AuditID = c.Guid(nullable: false),
                        UserName = c.String(),
                        IPAddress = c.String(),
                        AreaAccessed = c.String(),
                        Timestamp = c.DateTime(nullable: false),
                        ResponesObject = c.String(),
                    })
                .PrimaryKey(t => t.AuditID);
            
            AddColumn("dbo.Histories", "esclation", c => c.Int());
            AddColumn("dbo.ProjectMembers", "PullDev", c => c.Int());
            AddColumn("dbo.ProjectMembers", "PullPa", c => c.Int());
            AddColumn("dbo.ProjectMembers", "DoneDev", c => c.Int());
            AddColumn("dbo.ProjectMembers", "RejectPa", c => c.Int());
            AddColumn("dbo.ProjectMembers", "DonePa", c => c.Int());
            AddColumn("dbo.Tickets", "PullDev", c => c.Int());
            AddColumn("dbo.Tickets", "PullPa", c => c.Int());
            AddColumn("dbo.Tickets", "DoneDev", c => c.Int());
            AddColumn("dbo.Tickets", "RejectPa", c => c.Int());
            AddColumn("dbo.Tickets", "DonePa", c => c.Int());
            AlterColumn("dbo.Tickets", "Status", c => c.Int(nullable: false));
            DropColumn("dbo.ProjectMembers", "Step");
        }
    }
}
