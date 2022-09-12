using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketinDataAccess.Entity.data;

namespace businesslogic.Dto
{
    public class permissionsDto
    {
        public int Id { set; get; }
        public String Name { set; get; }
        public List<Permission_UserRoleDto> lipermission_UserRoles { set; get; }



    }
}
