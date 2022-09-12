using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketinsystems.data;

namespace TicketinDataAccess.Repository
{
   public interface IRepositoryEmpolyee
    {
        IEnumerable<Employee> GetALL();
        IEnumerable<Employee> GetALLD();
        IEnumerable<Employee> GetAllEmp();


    }
}
