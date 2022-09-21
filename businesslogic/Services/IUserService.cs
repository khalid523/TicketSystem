using businesslogic.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketinsystems.data;

namespace businesslogic.Services
{
   public interface IUserService
    {
        void Insert(UsersDato usersDato);
        void Delete(int Id);
        List<UsersDato> LoadAll();

    }
}
