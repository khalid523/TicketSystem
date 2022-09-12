using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ticketinsystems.data
{
    public class login
    {
        public int id { set; get; }
        [Required]
        public string Email { set; get; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { set; get; }
    }
}