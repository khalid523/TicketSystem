using businesslogic.Services;
using System.Web.Http;
using TicketinDataAccess.Repository;
using Ticketinsystems.data;
using TicketinDataAccess.Entity.data;
using Unity;
using Unity.WebApi;

namespace WebApi
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            container.RegisterType<IRoleService, RoleService>();
            container.RegisterType<IpermissionsService, permissionsService>();
            container.RegisterType<IPermission_UserRoleService, Permission_UserRoleService>();
            container.RegisterType<IClientService, ClientService>();
            container.RegisterType<IDepartmentService, DepartmentService>();
            container.RegisterType<ITicketService, TicketService>();
            container.RegisterType<IEmpolyeeServices, EmpolyeeServices>();
            container.RegisterType<IUserService, UserService>();
            container.RegisterType<IRepository<Employee>, Repository<Employee>>();
            container.RegisterType<IRepository<permissions>, Repository<permissions>>();
            container.RegisterType<IRepository<Department>, Repository<Department>>();
            container.RegisterType<IRepositoryEmpolyee, RepositoryEmpolyee>();
            container.RegisterType<IRepositoryUser, RepositoryUser>();
            container.RegisterType<IRepositoryHistory, RepositoryHistory>();
            container.RegisterType<IRepositoryProject, RepositoryProject>();
            container.RegisterType<IPmService, PmService>();
            container.RegisterType<IRepository<ProjectMember>, Repository<ProjectMember>>();
            container.RegisterType<IRepository<Users>, Repository<Users>>();
            container.RegisterType<IRepository<Projects>, Repository<Projects>>();
            container.RegisterType<IRepository<UserRole>, Repository<UserRole>>();
            container.RegisterType<IRepository<Client>, Repository<Client>>();
            container.RegisterType<IRepositoryClient, RepositoryClient>();
            container.RegisterType<IProjectService, ProjectService>();
            container.RegisterType<IRepository<History>, Repository<History>>();
            container.RegisterType<IUserRole, UserRoleService>();
            container.RegisterType<IRepositoryPM, RepositoryPM>();
            container.RegisterType<IRepositoryTickets, RepositoryTickets>();
            container.RegisterType<IRepostitoryPermission_UserRole, RepostitoryPermission_UserRole>();
            container.RegisterType<IRepository<Permission_UserRole>, Repository<Permission_UserRole>>();
            container.RegisterType<IRepository<Permission_UserRole>, Repository<Permission_UserRole>>();
            container.RegisterType<IHistoryService, HistoryService>();
            container.RegisterType<IRepository<History>, Repository<History>>();
            container.RegisterType<IRepository<Tickets>, Repository<Tickets>>();
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}