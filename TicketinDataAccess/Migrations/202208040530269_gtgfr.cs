namespace TicketinDataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class gtgfr : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tickets", "isLeader", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tickets", "isLeader");
        }
    }
}
