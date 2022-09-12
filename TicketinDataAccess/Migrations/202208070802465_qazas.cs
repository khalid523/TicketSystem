namespace TicketinDataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class qazas : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Histories", "EscLeader", c => c.Boolean(nullable: false));
            DropColumn("dbo.Tickets", "EscLeader");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tickets", "EscLeader", c => c.Boolean(nullable: false));
            DropColumn("dbo.Histories", "EscLeader");
        }
    }
}
