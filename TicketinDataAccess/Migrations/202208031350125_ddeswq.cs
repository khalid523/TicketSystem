namespace TicketinDataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ddeswq : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Histories", "project", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Histories", "project");
        }
    }
}
