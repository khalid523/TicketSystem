using businesslogic.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace businesslogic.Services
{
    public interface IPmService
    {
       void Insert(ProjectMemberDto PmDto);
        IEnumerable<ProjectMemberDto> LoadALl();
        IEnumerable<ProjectMemberDto> LoadALLL();
    }
}
