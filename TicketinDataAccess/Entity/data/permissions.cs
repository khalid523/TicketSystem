using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TicketinDataAccess.Entity.data;

namespace Ticketinsystems.data
{
    public class permissions
    {
        public int Id { set; get; }
        public String Name { set; get; }
        public List<Permission_UserRole> lipermission_UserRoles { set; get; }
       

    }
}