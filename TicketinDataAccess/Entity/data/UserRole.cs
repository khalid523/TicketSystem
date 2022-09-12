using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TicketinDataAccess.Entity.data;

namespace Ticketinsystems.data
{
    public class UserRole
    {
        [Key]
        public int RoleId { set; get; }
        [Required]
        public string RoleName { set; get; }
        public List<Users> Users { set; get; }
        public List<Employee> liemployees { set; get; }
        public List<Client> liclients { set; get; }
       
        public List<Permission_UserRole> lipermission_UserRoles { set; get; }


    }
}