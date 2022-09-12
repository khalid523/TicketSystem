namespace TicketinDataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adadq : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProjectMembers", "Step", c => c.String());
            AlterColumn("dbo.Tickets", "Status", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tickets", "Status", c => c.Int(nullable: false));
            DropColumn("dbo.ProjectMembers", "Step");
        }
    }
}
