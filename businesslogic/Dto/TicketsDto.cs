using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketinsystems.data;

namespace businesslogic.Dto
{
   public class TicketsDto
    {

        public int Id { get; set; }
        public string Descriptions { set; get; }
        public string Status { set; get; }
        public int? UserId { set; get; }
        public virtual UsersDato User { set; get; }
        public int ProjectsId { set; get; }
        public virtual ProjectsDto Projects { set; get; }
        public int? AssgintoId { set; get; }
        public virtual UsersDato Assginto { set; get; }
        public bool Esc { set; get; }
    }
}
