using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketinsystems.data;

namespace TicketinDataAccess.Repository
{
    public class RepositoryUser: IRepositoryUser
    {
        private readonly TicketinContext context;
        public RepositoryUser(TicketinContext _context)
        {

            //this.context = context;
            context = _context;
        }

        public List<Users> GetALL()
        {
            try
            {
                return context.users.Include("Role").ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
