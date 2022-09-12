namespace TicketinDataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class aqaz : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Histories", "IsUrgent", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Histories", "IsUrgent");
        }
    }
}
