using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketinsystems.data;

namespace TicketinDataAccess.Entity.data
{
    public class Permission_UserRole
    {
        [ForeignKey("Permissions")]
        public int? PermissionsId { set; get; }
        public virtual permissions Permissions { set; get; }

        [ForeignKey("userRole")]
        public int? userRoleId { set; get; }
        public UserRole userRole { set; get; }


    }
}
