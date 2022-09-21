using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Ticketinsystems.data;

namespace TicketinDataAccess.Repository
{
    public class RepositoryEmpolyee : IRepositoryEmpolyee
    {
        private readonly TicketinContext context;

        public RepositoryEmpolyee(TicketinContext _context)
        {
            
            context = _context;
        }


        public IEnumerable<Employee> GetALL()
        {
            try
            {
                return context.Employees.Include(x => x.User.ProjectMembers.Select(y => y.Projects)).Include("Role").Include("department").Include("user").ToList();
               
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public IEnumerable<Employee> GetALLD()
        {
            try
            {
                return context.Employees.Include(x => x.projects).Include(x => x.Role).Include(x => x.department).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public IEnumerable<Employee> GetAllEmp()
        {
            try
            {
                return context.Employees.Include(x => x.department).Include(x => x.department);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        

    }
}
