using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketinsystems.data;
using System.Data.Entity;

namespace TicketinDataAccess.Repository
{
   public class RepositoryTickets:IRepositoryTickets
    {

        private readonly TicketinContext context;
        public RepositoryTickets(TicketinContext _context)
        {

            //this.context = context;
            context = _context;
        }
        public List<Tickets> GetALLTickets()
        {
            try
            {
                return context.tickets.Include(x => x.User).Include(x => x.Projects).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
   
        


    }
}
