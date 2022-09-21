using businesslogic.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketinsystems.data;

namespace businesslogic.Services
{
   public interface IProjectService
    {
        List<ProjectsDto> Load();
        void Insert(ProjectsDto projectsDto);
        void Delete(int Id);
        ProjectsDto Edit(int Id);
        void update(ProjectsDto projectsDto);
        List<ProjectMemberDto> LoadAll();
        ProjectsDto load1(int Id);

    }
}
