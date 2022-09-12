using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketinDataAccess.Entity.data;
using Ticketinsystems.data;

namespace businesslogic.Dto
{
   public class UserRoleDto
    {

        public int RoleId { set; get; }
        public string RoleName { set; get; }
        public List<UsersDato> Users { set; get; }
        public List<EmpolyeeDto> liemployees { set; get; }
        public List<ClientDto> liclients { set; get; }

        public List<Permission_UserRoleDto> lipermission_UserRoles { set; get; }
    }
}
