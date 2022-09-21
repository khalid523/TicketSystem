using businesslogic.Dto;
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
    public class Permission_UserRoleService : IPermission_UserRoleService
    {
        private readonly IRepository<Permission_UserRole> repository;
        private readonly IRepostitoryPermission_UserRole repostitoryPermission_UserRole;
        private readonly IRepository<permissions> repositoryPermissions;
        public Permission_UserRoleService(IRepository<Permission_UserRole> _repository, IRepostitoryPermission_UserRole _repostitoryPermission_UserRole, IRepository<permissions> _repositoryPermissions)
        {
            repository = _repository;
            repostitoryPermission_UserRole = _repostitoryPermission_UserRole;
            repositoryPermissions = _repositoryPermissions;
        }
        public void Insert(Permission_UserRoleDto permission_UserRoleDto)
        {
            Permission_UserRole Dept = new Permission_UserRole();
            Dept.PermissionsId = permission_UserRoleDto.PermissionsId;
            Dept.userRoleId = permission_UserRoleDto.userRoleId;
            repository.Insert(Dept);


        }
        public IEnumerable<Permission_UserRoleDto> Load()
        {

            List<Permission_UserRoleDto> liDeto = new List<Permission_UserRoleDto>();
            List<Permission_UserRole> lidepartment = repostitoryPermission_UserRole.GetALL().ToList();
            foreach (var item in lidepartment)
            {
                Permission_UserRoleDto permissionsDto = new Permission_UserRoleDto();
                permissionsDto.Permissions = new permissionsDto();
                permissionsDto.userRole = new UserRoleDto();
                permissionsDto.Permissions.Name = item.Permissions.Name;
                permissionsDto.userRole.RoleName = item.userRole.RoleName;
                permissionsDto.userRoleId = item.userRoleId;
                permissionsDto.PermissionsId = item.PermissionsId;

                permissionsDto.Permissions.Id = repositoryPermissions.Load((int)permissionsDto.PermissionsId).Id;
                permissionsDto.Permissions.Name = repositoryPermissions.Load((int)permissionsDto.PermissionsId).Name;
                liDeto.Add(permissionsDto);
            }
            return liDeto;

        }
    }
}
