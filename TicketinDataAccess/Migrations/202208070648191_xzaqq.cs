namespace TicketinDataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class xzaqq : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tickets", "Esc", c => c.Boolean(nullable: false));
            DropColumn("dbo.Histories", "check");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Histories", "check", c => c.Boolean(nullable: false));
            DropColumn("dbo.Tickets", "Esc");
        }
    }
}
