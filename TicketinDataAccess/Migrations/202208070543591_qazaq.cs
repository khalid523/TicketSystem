namespace TicketinDataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class qazaq : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Histories", "check", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Histories", "check");
        }
    }
}
