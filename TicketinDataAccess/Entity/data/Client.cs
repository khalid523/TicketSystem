using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Ticketinsystems.data
{
    public class Client
    {
        public int Id { set; get; }
      
        public string Name { set; get; }

        [Required(ErrorMessage = "please Fill the Email !!")]
        public string Email { set; get; }

        [Required(ErrorMessage = "please Fill the Password !!")]
        public string Password { set; get; }
        public string Informations { set; get; }

        [ForeignKey("User")]
        public int? UserId { set; get; }
        public Users User { set; get; }

        [ForeignKey("projects")]
        public int? projectsId { set; get; }
        public Projects projects { set; get; }

        [ForeignKey("Role")]
        public int? RoleId { set; get; }
        public UserRole Role { set; get; }
        public bool isDelete { set; get; }



    }
}