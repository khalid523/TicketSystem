using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace businesslogic.Services
{
   public interface IRoleService
    {
        string[] GetRoles(string username);
    }
}
