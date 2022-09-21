using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketinDataAccess.Entity.data;
using Ticketinsystems.data;

namespace TicketinDataAccess.Repository
{
   public class RepostitoryPermission_UserRole : IRepostitoryPermission_UserRole
    {

        private readonly TicketinContext context;
        public RepostitoryPermission_UserRole(TicketinContext _context)
        {

            //this.context = context;
            context = _context;
        }


        public List<Permission_UserRole> GetALL()
        {
            try
            {
                return context.lipermission_UserRoles.Include("Permissions").Include("userRole").ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
