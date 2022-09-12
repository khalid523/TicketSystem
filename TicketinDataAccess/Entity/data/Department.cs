using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace Ticketinsystems.data
{
    public class Department
    {
        [Key] 
        public int Id { set; get; }
        [Required(ErrorMessage ="please fill the Name !!!")]
        public string Name { set; get; }
        public List<Employee> liemployees { set; get; }

    }
}