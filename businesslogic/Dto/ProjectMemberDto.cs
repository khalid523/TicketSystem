using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketinsystems.data;

namespace businesslogic.Dto
{
    public class ProjectMemberDto
    {
        public int? UserId { set; get; }
        public virtual UsersDato User { set; get; }
        public int? ProjectsId { set; get; }
        public ProjectsDto Projects { set; get; }
        public string Step { set; get; }

        public bool IsLader { set; get; }
    }
}
