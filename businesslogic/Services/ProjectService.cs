using businesslogic.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketinDataAccess.Entity.data;
using TicketinDataAccess.Repository;
using Ticketinsystems.data;

namespace businesslogic.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IRepository<Projects> repository;
        private readonly IRepository<Client> repositoryClient;
        private readonly IRepository<Employee> repositoryEmployee;
        private readonly IRepository<History> repositoryHistory;
        private readonly IRepositoryPM repositoryPM;
        private readonly IRepository<ProjectMember> repositoryProjectM;
        private readonly IRepositoryProject repositoryProject;
        public ProjectService(IRepository<Projects> _repository, IRepository<Client> _repositoryClient, IRepository<Employee> _repositoryEmployee, IRepository<History> _repositoryHistory,IRepositoryPM _repositoryPM, IRepository<ProjectMember>  _repositoryProjectM,IRepositoryProject _repositoryProject)
        {
            repository = _repository;
            repositoryClient = _repositoryClient;
            repositoryEmployee = _repositoryEmployee;
            repositoryHistory = _repositoryHistory;
            repositoryPM = _repositoryPM;
            repositoryProjectM = _repositoryProjectM;
            repositoryProject = _repositoryProject;
        }

        public List<ProjectsDto> Load()
        {

            List<ProjectsDto> liDeto = new List<ProjectsDto>();
            List<Projects> liprojects = repository.LoadAll().ToList();
            foreach (var item in liprojects)
            {
                ProjectsDto projectsDto = new ProjectsDto();
                projectsDto.Id = item.Id;
                projectsDto.Name = item.Name;
                liDeto.Add(projectsDto);
            }
            return liDeto;

        }
        public List<ProjectMemberDto> LoadAll()
        {
            List<ProjectMemberDto> liDeto = new List<ProjectMemberDto>();
            IEnumerable<ProjectMember> liprojects = repositoryPM.GetALL();
            foreach (var item in liprojects)
            {
                ProjectMemberDto projectMemberDto = new ProjectMemberDto();
                projectMemberDto.Projects = new ProjectsDto();
                projectMemberDto.User = new UsersDato();
                projectMemberDto.IsLader = item.IsLader;
                projectMemberDto.UserId = item.UserId;
                projectMemberDto.ProjectsId = item.ProjectsId;
                projectMemberDto.User.Name = item.User.Name;
                projectMemberDto.User.Id = item.User.Id;
                liDeto.Add(projectMemberDto);
            }
            return liDeto;
        }
        public void Delete(int Id)
        {
            Projects projects = repository.Load(Id);
            var clientProjectId = projects.Id;
            var clientId = repositoryClient.LoadAll().Where(c => c.projectsId == clientProjectId).Select(c => c.Id).ToList();
            var UserIdClient = repositoryClient.LoadAll().Where(c => c.projectsId == clientProjectId).Select(c => c.UserId).ToList();
            var EmpolyeeId = repositoryEmployee.LoadAll().Where(c => c.projectsId == clientProjectId).Select(c => c.Id).ToList();
            var UserIdEmpolyee = repositoryEmployee.LoadAll().Where(c => c.projectsId == clientProjectId).Select(c => c.UserId).ToList();
            var HistoryRelation = repositoryHistory.LoadAll().Where(c => c.ProjectsId == clientProjectId).Select(c => c.Id).ToList();

            foreach (var item in clientId)
            {
                Client client = new Client();
                client=   repositoryClient.Load(item);
                client.projectsId = null;
                repositoryClient.Update(client);
            }
            foreach (var item in HistoryRelation)
            {
                History history = new History();
                history = repositoryHistory.Load(item);
                history.ProjectsId= null;
                repositoryHistory.Update(history);
            }

            foreach (var item in EmpolyeeId)
            {
                Employee Emp = repositoryEmployee.Load(item);
                Emp.projectsId = null;
                repositoryEmployee.Update(Emp);
            }

            repository.Delete(Id);
        }
        public ProjectsDto load1(int Id)
        {
            ProjectsDto dto = new ProjectsDto();
            dto.Id = repository.Load(Id).Id;
            dto.Name = repository.Load(Id).Name;

            return dto;

        }

        public ProjectsDto Edit(int Id)
        {

            return this.load1(Id);
        }

        public void Insert(ProjectsDto projectsDto)
        {


            Projects projects = new Projects();
            projects.Id = projectsDto.Id;
            projects.Name = projectsDto.Name;
            repository.Insert(projects);
        }

        public void update(ProjectsDto projectsDto)
        {
            Projects projects = repository.Load(projectsDto.Id);
            var projectmP = repositoryProjectM.LoadAll().Where(x => x.UserId == projectsDto.employeeId).FirstOrDefault();
            projectmP.IsLader = true;
            repositoryProjectM.Update(projectmP);
            var clientProjectId = repositoryClient.LoadAll().Where(c => c.projectsId == projects.Id).Select(c => c.Id).ToList();
            var EmployeeProjectId = repositoryEmployee.LoadAll().Where(c => c.projectsId == projects.Id).Select(c => c.Id).ToList();
            var HistoryRelation = repositoryHistory.LoadAll().Where(c => c.ProjectsId == projects.Id).Select(c => c.Id).ToList();
            Projects projectss = new Projects();
            projectss.Name = projectsDto.Name;
            projectss.Id = projectsDto.Id;
            repositoryProject.update(projectss);
        }
    }
}
