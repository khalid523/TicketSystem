using businesslogic.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketinsystems.data;

namespace businesslogic.Services
{
   public interface IDepartmentService
    {
        List<DepartmentDto> Load();
        void Insert(DepartmentDto DeptDto);
        void Delete(int Id);
        DepartmentDto Edit(int Id);
        void update(DepartmentDto DeptDto);
    }
}
