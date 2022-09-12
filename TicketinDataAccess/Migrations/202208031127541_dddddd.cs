namespace TicketinDataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dddddd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProjectMembers", "IsLader", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProjectMembers", "IsLader");
        }
    }
}
