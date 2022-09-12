using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketinsystems.data;

namespace TicketinDataAccess.Repository
{
   public interface IRepositoryClient
    {
        List<Client> GetALL();
        List<Client> GetALLClient();
    }
}
