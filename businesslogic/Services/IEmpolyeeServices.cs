using businesslogic.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketinsystems.data;
namespace businesslogic.Services
{
   public interface IEmpolyeeServices
    {
        List<EmpolyeeDto> Load();
        void Insert(EmpolyeeDto EmpDto, int[] projectsId, int RoleId, int departmentid);
        void Delete(int Id);
        EmpolyeeDto Edit(int Id);
        void update(EmpolyeeDto empolyeeDto, int[] projectsId, int RoleId, int departmentid);
        List<EmpolyeeDto> empolyeeDtos();
    }
}
