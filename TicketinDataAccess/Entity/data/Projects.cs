using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ticketinsystems.data
{
    public class Projects
    {

        public int Id { set; get; }
        public string Name { set; get; }
        public virtual ICollection<Tickets> litickets { set; get; }
        public virtual ICollection<ProjectMember> ProjectMembers { set; get; }

        //[ForeignKey("employee")]
        //public int? employeeId { set; get; }
        //public Employee employee { set; get; }
    }
}