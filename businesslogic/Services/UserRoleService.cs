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
   public class UserRoleService:IUserRole
    {
        private readonly IRepository<UserRole> repositoryUserRole;
        private readonly IPermission_UserRoleService permission_UserRoleService;
        private readonly IRepository<Permission_UserRole> repositoryPermissiinUserRole;
        public UserRoleService(IRepository<UserRole> _repositoryUserRole,IPermission_UserRoleService _permission_UserRoleService, IRepository<Permission_UserRole> _repositoryPermissiinUserRole)
        {
            repositoryUserRole = _repositoryUserRole;
            permission_UserRoleService = _permission_UserRoleService;
            repositoryPermissiinUserRole = _repositoryPermissiinUserRole;
        }
        public List<UserRoleDto> Load()
        {

            List<UserRoleDto> liDeto = new List<UserRoleDto>();
            List<UserRole> liuserRoles = repositoryUserRole.LoadAll().ToList();
            foreach (var item in liuserRoles)
            {
                UserRoleDto userRoleDto = new UserRoleDto();
                userRoleDto.RoleId = item.RoleId;
                userRoleDto.RoleName = item.RoleName;
                liDeto.Add(userRoleDto);
            }
            return liDeto;

        }


        public void Insert(UserRoleDto userRoleDto, int[] permissionList)
        {
            Permission_UserRole pr = new Permission_UserRole();
            UserRole userRole = new UserRole();
            userRole.RoleId = userRoleDto.RoleId;
            userRole.RoleName = userRoleDto.RoleName;
            repositoryUserRole.Insert(userRole);
            foreach (var item in permissionList)
            {
                Permission_UserRoleDto prDto = new Permission_UserRoleDto();
                prDto.userRoleId = userRole.RoleId;
                prDto.PermissionsId = item;
                permission_UserRoleService.Insert(prDto);
            }
        }


        public void Delete(int Id)
        {
            repositoryUserRole.Delete(Id);
          
        }
        public UserRoleDto Edit(int Id)
        {
            var userRole = repositoryUserRole.Load(Id);
            UserRoleDto userRoleDto = new UserRoleDto();
            userRoleDto.RoleName = userRole.RoleName;
            userRoleDto.RoleId = userRole.RoleId;
            return userRoleDto;
        }
        public void update(UserRoleDto userRoleDto, int[] permissionList)
        {
            if (permissionList != null)
            {

                var permissionRole = repositoryPermissiinUserRole.LoadAll().Where(p => p.userRoleId ==userRoleDto.RoleId).FirstOrDefault();
                var pm = repositoryPermissiinUserRole.LoadAll().Where(p => p.userRoleId == permissionRole.userRoleId).ToList();

                foreach (var item in pm)
                {

                    repositoryPermissiinUserRole.Deletet(item);
                }


                Permission_UserRoleDto pmm = new Permission_UserRoleDto();
                Permission_UserRole permission_UserRole = new Permission_UserRole();
                foreach (var item in permissionList)
                {
                   
                    pmm.userRoleId = userRoleDto.RoleId;
                    pmm.PermissionsId = item;
                    permission_UserRole.userRoleId = pmm.userRoleId;
                    permission_UserRole.PermissionsId = pmm.PermissionsId;
                    repositoryPermissiinUserRole.Insert(permission_UserRole);
                }
                UserRole userRole = new UserRole();
                userRole.RoleId = userRoleDto.RoleId;
                userRole.RoleName = userRoleDto.RoleName;
                repositoryUserRole.Update(userRole);
            }
        }

    }
}
