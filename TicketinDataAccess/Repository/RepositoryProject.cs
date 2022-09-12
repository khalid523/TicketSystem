using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketinsystems.data;

namespace TicketinDataAccess.Repository
{
    public class RepositoryProject:IRepositoryProject
    {
        TicketinContext db = new TicketinContext();
        public void update(Projects  projects)
        {
            db.projects.Attach(projects);
            db.Entry(projects).State = EntityState.Modified;
            db.SaveChanges();

        }
    }
}
