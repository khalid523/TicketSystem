using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Ticketinsystems.data
{
    public class Tickets

    {
        public int Id { get; set; }
        public string Descriptions { set; get; }
        public string Status { set; get; }
        [ForeignKey("User")]
        public int? UserId { set; get; }
        public virtual Users User { set; get; }
        public int ProjectsId { set; get; }
        public virtual Projects Projects { set; get; }
        [ForeignKey("Assginto")]
        public int? AssgintoId { set; get; }
        public virtual Users Assginto { set; get; }
        public bool Esc { set; get; }

       
  

    }
}