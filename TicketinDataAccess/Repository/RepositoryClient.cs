using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Ticketinsystems.data;

namespace TicketinDataAccess.Repository
{
   public class RepositoryClient :IRepositoryClient
    {
        private readonly TicketinContext context;
        public RepositoryClient(TicketinContext _context)
        {

           
            context = _context;
        }


        public List<Client> GetALL()
        {
            try
            {
                return context.Clients.Include(x => x.User.ProjectMembers.Select(y => y.Projects)).Include("Role").Include("user").ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<Client> GetALLClient()
        {
            try
            {
                return context.Clients.Include(x => x.projects).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
