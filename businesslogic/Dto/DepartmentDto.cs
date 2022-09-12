using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketinsystems.data;

namespace businesslogic.Dto
{
   public class DepartmentDto
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public List<EmpolyeeDto> liemployees { set; get; }


    }
}
