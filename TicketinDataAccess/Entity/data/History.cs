using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketinsystems.data;

namespace TicketinDataAccess.Entity.data
{
   public class History
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public DateTime Date { set; get; }
        public int? ProjectsId { set; get; }
        public virtual Projects Projects { set; get; }

        public string Description { set; get; }
        public bool IsUrgent { set; get; }

        public string Status { set; get; }
         public bool EscLeader { set; get; }

    }
}
