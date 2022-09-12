using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Ticketinsystems.data
{
    public class ProjectMember
    {

        [ForeignKey("User")]
        public int? UserId { set; get; }
        public virtual Users User { set; get; }

        [ForeignKey("Projects")]
        public int? ProjectsId { set; get; }
        public Projects Projects { set; get; }
        public string Step { set; get; }

        public bool IsLader { set; get; }

    }
}