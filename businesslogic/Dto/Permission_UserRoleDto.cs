using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketinsystems.data;

namespace businesslogic.Dto
{
    public class Permission_UserRoleDto
    {
        public int? PermissionsId { set; get; }
        public virtual permissionsDto Permissions { set; get; }
        public int? userRoleId { set; get; }
        public UserRoleDto userRole { set; get; }
    }
}
