using businesslogic.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace businesslogic.Services
{
    public interface IPermission_UserRoleService
    {
        void Insert(Permission_UserRoleDto permission_UserRoleDto);
        IEnumerable<Permission_UserRoleDto> Load();
    }
}
