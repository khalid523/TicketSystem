namespace TicketinDataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class qaz : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clients", "isDelete", c => c.Boolean(nullable: false));
            AddColumn("dbo.Users", "isDelete", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "isDelete");
            DropColumn("dbo.Clients", "isDelete");
        }
    }
}
