using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Ticketinsystems.data
{
    public class Users
    {

        public int? Id { set; get; }
        [Required(ErrorMessage = "please Fill the name !!")]
        public string Name { set; get; }
        [Required(ErrorMessage = "please Fill the Email !!")]
        public string Email { set; get; }

        [Required(ErrorMessage = "please Fill the Password !!")]
        public string Password { set; get; }
        public string Phone { set; get; }

        [ForeignKey("Role")]
        public int? RoleId { set; get; }
        public UserRole Role { set; get; }
        public virtual ICollection<Tickets> litickets { set; get; }
        public virtual ICollection<ProjectMember> ProjectMembers { set; get; }
        public bool isDelete { set; get; }

    }
}