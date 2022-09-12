using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketinsystems.data;

namespace businesslogic.Dto
{
   public class ProjectsDto
    {

        public int Id { set; get; }
        public string Name { set; get; }
        public virtual ICollection<TicketsDto> litickets { set; get; }
        public virtual ICollection<ProjectMemberDto> ProjectMembers { set; get; }
        public int? employeeId { set; get; }
        public EmpolyeeDto employee { set; get; }
    }
}
