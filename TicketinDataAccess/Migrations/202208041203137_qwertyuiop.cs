namespace TicketinDataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class qwertyuiop : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Histories", "Status", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Histories", "Status");
        }
    }
}
