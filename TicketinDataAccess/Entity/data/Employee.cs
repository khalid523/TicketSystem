using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Ticketinsystems.data
{
    public class Employee
    {

        public int Id { set; get; }
        public string Name { set; get; }
        [Required]
        public string Salary { set; get; }

        [Required(ErrorMessage = "please Fill the Email !!")]
        public string Email { set; get; }

        [Required(ErrorMessage = "please Fill the Password !!")]
        public string Password { set; get; }

        [ForeignKey("User")]
        public int? UserId { set; get; }
        public Users User { set; get; }

        [ForeignKey("projects")]
        public int? projectsId { set; get; }
        public Projects projects { set; get; }

        [ForeignKey("department")]
        public int? departmentid { set; get; }
        public  Department department { set; get; }


        [ForeignKey("Role")]
        public int? RoleId { set; get; }
        public UserRole Role { set; get; }

        public bool isDelete { set; get; }

        public List<Projects> liprojects { set; get; }


    }
}