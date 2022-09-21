using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketinsystems.data;
using System.Data.Entity;

namespace TicketinDataAccess.Repository
{
   public class RepositoryPM:IRepositoryPM
    {
        private readonly TicketinContext context;
        public RepositoryPM(TicketinContext _context)
        {
           
            context = _context;
        }


        public IEnumerable<ProjectMember> GetALL()
        {
            try
            {
                return context.projectMembers.Include(x => x.User).Include(x=>x.Projects).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public IEnumerable<ProjectMember> GetALLL()
        {
            try
            {
                return context.projectMembers.ToList();
                // return _context.PermissionUser.Include(x => x.Login).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

  

       


    }
}
