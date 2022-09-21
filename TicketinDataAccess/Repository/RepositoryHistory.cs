using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketinDataAccess.Entity.data;
using Ticketinsystems.data;

namespace TicketinDataAccess.Repository
{
   public class RepositoryHistory:IRepositoryHistory
    {
        private readonly TicketinContext context;
        public RepositoryHistory(TicketinContext _context)
        {

            //this.context = context;
            context = _context;
        }
        public List<History> GetALL()
        {
            try
            {
                return context.History.Include(x=>x.Projects).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
