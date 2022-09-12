namespace TicketinDataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class aaaaa : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Projects", "employeeId", "dbo.Employees");
            DropIndex("dbo.Projects", new[] { "employeeId" });
            DropColumn("dbo.Projects", "employeeId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Projects", "employeeId", c => c.Int());
            CreateIndex("dbo.Projects", "employeeId");
            AddForeignKey("dbo.Projects", "employeeId", "dbo.Employees", "Id");
        }
    }
}
