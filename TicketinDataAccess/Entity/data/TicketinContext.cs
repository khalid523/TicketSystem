using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TicketinDataAccess.Entity.data;

namespace Ticketinsystems.data
{
    public class TicketinContext:DbContext
    {
        public TicketinContext() : base("DefaultConnection")
        {

        }
        public DbSet<Users> users { set; get; }
        public DbSet<UserRole> userRoles { set; get; }
        public DbSet<permissions> permissions { set; get; }
        public DbSet<Permission_UserRole> lipermission_UserRoles { set; get; }
        
        public DbSet<Department> departments { set; get; }
        public DbSet<Projects> projects { set; get; }
        public DbSet<Tickets> tickets { set; get; }
        public DbSet<Client> Clients { set; get; }
        public DbSet<LookupStatus> lookupStatuses { set; get; }
       
        public DbSet<ProjectMember> projectMembers { set; get; }
        

        public DbSet<Employee> Employees { set; get; }
        public DbSet<History> History { set; get; }
       
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tickets>().HasKey(T => T.Id);
            modelBuilder.Entity<ProjectMember>().HasKey(T => new { T.UserId, T.ProjectsId });
            modelBuilder.Entity<Permission_UserRole>().HasKey(P => new { P.PermissionsId, P.userRoleId });
            base.OnModelCreating(modelBuilder);
        }

     
    }
}