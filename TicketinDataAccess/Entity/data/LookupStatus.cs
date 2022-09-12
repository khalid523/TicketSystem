using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Ticketinsystems.data
{
    [Table("Status", Schema = "Lookup")]
    public class LookupStatus
    {

        public int Id { set; get; }
        public string Name { set; get; }
        //[ForeignKey("status")]
        //public int? statusId { set; get; }
        //public Status status { set; get; }

    }
}