namespace TicketinDataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dddsadas : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Projects", "employeeId", c => c.Int());
            AddColumn("dbo.Projects", "Employee_Id", c => c.Int());
            CreateIndex("dbo.Projects", "employeeId");
            CreateIndex("dbo.Projects", "Employee_Id");
            AddForeignKey("dbo.Projects", "Employee_Id", "dbo.Employees", "Id");
            AddForeignKey("dbo.Projects", "employeeId", "dbo.Employees", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Projects", "employeeId", "dbo.Employees");
            DropForeignKey("dbo.Projects", "Employee_Id", "dbo.Employees");
            DropIndex("dbo.Projects", new[] { "Employee_Id" });
            DropIndex("dbo.Projects", new[] { "employeeId" });
            DropColumn("dbo.Projects", "Employee_Id");
            DropColumn("dbo.Projects", "employeeId");
        }
    }
}
