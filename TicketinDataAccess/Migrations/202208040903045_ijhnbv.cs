namespace TicketinDataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ijhnbv : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Histories", "project");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Histories", "project", c => c.String());
        }
    }
}
