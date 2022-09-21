using businesslogic.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketinDataAccess.Repository;
using Ticketinsystems.data;

namespace businesslogic.Services
{
    public class PmService: IPmService
    {
      
        private readonly IRepository<ProjectMember> repository;
        private readonly IRepositoryPM repositoryPM;
        public PmService(IRepository<ProjectMember> _repository, IRepositoryPM _repositoryPM)
        {
            this.repository = _repository;
            repositoryPM = _repositoryPM;
        }
        public void Insert(ProjectMemberDto PmDto)
        {  
                repository.Insert(new ProjectMember { UserId = PmDto.UserId, ProjectsId = PmDto.ProjectsId });
        }
        public IEnumerable<ProjectMemberDto> LoadALl()
        {
            IEnumerable<ProjectMemberDto> Pro = repository.LoadAll().Select(e => new ProjectMemberDto
            {
                UserId = e.UserId,
                ProjectsId =e.ProjectsId,                    
            });

            return Pro;
        }
        public IEnumerable<ProjectMemberDto> LoadALLL()
        {
            List<ProjectMemberDto> liDeto = new List<ProjectMemberDto>();
            List<ProjectMember> lidPm = repositoryPM.GetALL().ToList();
            foreach (var item in lidPm)
            {
                ProjectMemberDto projectMemberDto = new ProjectMemberDto();
                projectMemberDto.Projects = new ProjectsDto();
                projectMemberDto.User = new UsersDato();
                projectMemberDto.ProjectsId = item.ProjectsId;
                projectMemberDto.Projects.Id= item.Projects.Id;
                projectMemberDto.Projects.Name = item.Projects.Name;
                projectMemberDto.User.Name = item.User.Name;
                projectMemberDto.UserId = item.UserId;
                projectMemberDto.IsLader = item.IsLader;
                liDeto.Add(projectMemberDto);
            }
            return liDeto;
        }
    }
}
