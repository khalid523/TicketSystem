using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketinDataAccess.Entity.data;
using TicketinDataAccess.Repository;
using Ticketinsystems.data;

namespace businesslogic.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRepository<Users> repositoryUsers;
        private readonly IRepository<permissions> repositoryPermission;
        private readonly IRepository<Permission_UserRole> repositoryPermissionUserRole;
        public RoleService(IRepository<Users> _repositoryUsers, IRepository<permissions> _repositoryPermission, IRepository<Permission_UserRole> _repositoryPermissionUserRole)
        {
            repositoryUsers = _repositoryUsers;
            repositoryPermission = _repositoryPermission;
            repositoryPermissionUserRole = _repositoryPermissionUserRole;
        }
        public string[] GetRoles(string username)
        {
            var userRoles = (from user in repositoryUsers.LoadAll()

                             join permRol in repositoryPermissionUserRole.LoadAll()
                             on user.RoleId equals permRol.userRoleId

                             join perm in repositoryPermission.LoadAll()
                             on permRol.PermissionsId equals perm.Id
                             where user.Email == username
                             select perm.Name).ToArray();
            return userRoles;
        }
    }
}
