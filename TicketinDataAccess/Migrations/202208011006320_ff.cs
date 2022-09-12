namespace TicketinDataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ff : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        Informations = c.String(),
                        UserId = c.Int(),
                        projectsId = c.Int(),
                        RoleId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Projects", t => t.projectsId)
                .ForeignKey("dbo.UserRoles", t => t.RoleId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.projectsId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Tickets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descriptions = c.String(),
                        Status = c.Int(nullable: false),
                        UserId = c.Int(),
                        ProjectsId = c.Int(nullable: false),
                        AssgintoId = c.Int(),
                        Users_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Users_Id)
                .ForeignKey("dbo.Users", t => t.AssgintoId)
                .ForeignKey("dbo.Projects", t => t.ProjectsId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.ProjectsId)
                .Index(t => t.AssgintoId)
                .Index(t => t.Users_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        Phone = c.String(),
                        RoleId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserRoles", t => t.RoleId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.ProjectMembers",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        ProjectsId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.ProjectsId })
                .ForeignKey("dbo.Projects", t => t.ProjectsId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.ProjectsId);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        RoleId = c.Int(nullable: false, identity: true),
                        RoleName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.RoleId);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Salary = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        UserId = c.Int(),
                        projectsId = c.Int(),
                        departmentid = c.Int(),
                        RoleId = c.Int(),
                        isDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Departments", t => t.departmentid)
                .ForeignKey("dbo.Projects", t => t.projectsId)
                .ForeignKey("dbo.UserRoles", t => t.RoleId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.projectsId)
                .Index(t => t.departmentid)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Permission_UserRole",
                c => new
                    {
                        PermissionsId = c.Int(nullable: false),
                        userRoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PermissionsId, t.userRoleId })
                .ForeignKey("dbo.permissions", t => t.PermissionsId, cascadeDelete: true)
                .ForeignKey("dbo.UserRoles", t => t.userRoleId, cascadeDelete: true)
                .Index(t => t.PermissionsId)
                .Index(t => t.userRoleId);
            
            CreateTable(
                "dbo.permissions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "Lookup.Status",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Clients", "UserId", "dbo.Users");
            DropForeignKey("dbo.Clients", "RoleId", "dbo.UserRoles");
            DropForeignKey("dbo.Clients", "projectsId", "dbo.Projects");
            DropForeignKey("dbo.Tickets", "UserId", "dbo.Users");
            DropForeignKey("dbo.Tickets", "ProjectsId", "dbo.Projects");
            DropForeignKey("dbo.Tickets", "AssgintoId", "dbo.Users");
            DropForeignKey("dbo.Users", "RoleId", "dbo.UserRoles");
            DropForeignKey("dbo.Permission_UserRole", "userRoleId", "dbo.UserRoles");
            DropForeignKey("dbo.Permission_UserRole", "PermissionsId", "dbo.permissions");
            DropForeignKey("dbo.Employees", "UserId", "dbo.Users");
            DropForeignKey("dbo.Employees", "RoleId", "dbo.UserRoles");
            DropForeignKey("dbo.Employees", "projectsId", "dbo.Projects");
            DropForeignKey("dbo.Employees", "departmentid", "dbo.Departments");
            DropForeignKey("dbo.ProjectMembers", "UserId", "dbo.Users");
            DropForeignKey("dbo.ProjectMembers", "ProjectsId", "dbo.Projects");
            DropForeignKey("dbo.Tickets", "Users_Id", "dbo.Users");
            DropIndex("dbo.Permission_UserRole", new[] { "userRoleId" });
            DropIndex("dbo.Permission_UserRole", new[] { "PermissionsId" });
            DropIndex("dbo.Employees", new[] { "RoleId" });
            DropIndex("dbo.Employees", new[] { "departmentid" });
            DropIndex("dbo.Employees", new[] { "projectsId" });
            DropIndex("dbo.Employees", new[] { "UserId" });
            DropIndex("dbo.ProjectMembers", new[] { "ProjectsId" });
            DropIndex("dbo.ProjectMembers", new[] { "UserId" });
            DropIndex("dbo.Users", new[] { "RoleId" });
            DropIndex("dbo.Tickets", new[] { "Users_Id" });
            DropIndex("dbo.Tickets", new[] { "AssgintoId" });
            DropIndex("dbo.Tickets", new[] { "ProjectsId" });
            DropIndex("dbo.Tickets", new[] { "UserId" });
            DropIndex("dbo.Clients", new[] { "RoleId" });
            DropIndex("dbo.Clients", new[] { "projectsId" });
            DropIndex("dbo.Clients", new[] { "UserId" });
            DropTable("Lookup.Status");
            DropTable("dbo.permissions");
            DropTable("dbo.Permission_UserRole");
            DropTable("dbo.Departments");
            DropTable("dbo.Employees");
            DropTable("dbo.UserRoles");
            DropTable("dbo.ProjectMembers");
            DropTable("dbo.Users");
            DropTable("dbo.Tickets");
            DropTable("dbo.Projects");
            DropTable("dbo.Clients");
        }
    }
}
