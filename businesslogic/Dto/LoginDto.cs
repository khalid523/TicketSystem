using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace businesslogic.Dto
{
    public class LoginDto
    {

        public int id { set; get; }
        public string Email { set; get; }
        [DataType(DataType.Password)]
        public string Password { set; get; }
    }
}
