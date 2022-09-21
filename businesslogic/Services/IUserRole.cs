using businesslogic.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace businesslogic.Services
{
  public  interface IUserRole
    {
        List<UserRoleDto> Load();
        void Insert(UserRoleDto userRoleDto, int[] permissionList);
        void Delete(int Id);
        UserRoleDto Edit(int Id);
        void update(UserRoleDto userRoleDto, int[] permissionList);
    }
}
