using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketinsystems.data;

namespace businesslogic.Dto
{
   public class EmpolyeeDto
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public string Salary { set; get; }
        public string Email { set; get; }
        public string Password { set; get; }
        public int? UserId { set; get; }
        public UsersDato User { set; get; }
        public int? projectsId { set; get; }
        public ProjectsDto projects { set; get; }
        public int? departmentid { set; get; }
        public DepartmentDto department { set; get; }
        public int? RoleId { set; get; }
        public UserRoleDto Role { set; get; }

        public bool isDelete { set; get; }

        public List<ProjectsDto> liprojects { set; get; }
        



    }
}
