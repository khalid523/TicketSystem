using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketinDataAccess.Entity.data;

namespace TicketinDataAccess.Repository
{
   public interface IRepostitoryPermission_UserRole
    {
        List<Permission_UserRole> GetALL();

    }
}
