using businesslogic.Dto;
using businesslogic.Enum;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TicketinDataAccess.Entity.data;
using TicketinDataAccess.Repository;
using Ticketinsystems.data;

namespace businesslogic.Services
{
    public class TicketService : ITicketService
    {
        public TicketService(IRepositoryEmpolyee _repositoryEmpolyee, IRepository<ProjectMember> _repositoryPM, IRepositoryClient _repositoryClient, IRepository<Tickets> _repository, IRepositoryTickets _repositoryTicketss, IUserService _userService, IRepository<History> _repositoryhistory, IRepository<Client> _repositoryClients, IRepository<History> _repositoryHistory)
        {
            repositoryEmpolyee = _repositoryEmpolyee;
            repositoryPM = _repositoryPM;
            repositoryClient = _repositoryClient;
            repositoryTickets = _repositoryTicketss;
            userService = _userService;
            repositoryhistory = _repositoryhistory;
            repositoryClients = _repositoryClients;
            repositoryHistory = _repositoryHistory;
            repository = _repository;
        }
        TicketinContext db = new TicketinContext();
        private readonly IRepository<Employee> repositoryEmp;
        private readonly IRepositoryEmpolyee repositoryEmpolyee;
        private readonly IRepository<ProjectMember> repositoryPM;
        private readonly IRepositoryClient repositoryClient;
        private readonly IRepositoryTickets repositoryTickets;
        private readonly IUserService userService;
        private readonly IRepository<History> repositoryhistory;
        private readonly IRepository<Client> repositoryClients;
        private readonly IRepository<History> repositoryHistory;
        private readonly IRepository<Tickets> repository;
        public void Insert(TicketsDto ticketsDto, int ProjectsId, int userId)
        {
            ticketsDto.Status = Status.waitingforPa.ToString();
            ticketsDto.ProjectsId = ProjectsId;
            Tickets tickets = new Tickets();
            tickets.Descriptions = ticketsDto.Descriptions;
            tickets.Status = ticketsDto.Status;
            tickets.ProjectsId = ProjectsId;
            tickets.UserId = userId;
            repository.Insert(tickets);
        }
        public void update(Tickets T)
        {
            db.tickets.Attach(T);
            db.Entry(T).State = EntityState.Modified;
            db.SaveChanges();

        }
        public List<TicketsDto> ticketsDtos(int? userId)
        {

            Employee employee = new Employee();
            Client client = new Client();
            Projects projects = new Projects();
            employee = repositoryEmpolyee.GetAllEmp().FirstOrDefault(x => x.UserId == userId);
            var projectForEmpolye = repositoryPM.LoadAll().Where(p => p.UserId == userId).Select(P => P.Projects).ToList();
            client = repositoryClient.GetALLClient().Where(x => x.UserId == userId).FirstOrDefault();
            var tickets = repositoryTickets.GetALLTickets();

            if (employee != null)
            {
                if (employee.department != null)
                {

                    if (employee.department.Name == "pa")
                    {
                        var projectForEmpolyee = repositoryPM.LoadAll().Where(p => p.UserId == userId).Select(P => P.ProjectsId).ToList();
                        var TicketsForPa = from t1 in repository.LoadAll()
                                           join pe in repositoryPM.LoadAll()
                                           on t1.ProjectsId equals pe.ProjectsId
                                           where pe.UserId == userId && (t1.Status == Status.waitingforPa.ToString() || (t1.AssgintoId == userId && t1.Status == Status.Pendeing.ToString()))
                                           select t1;
                        List<TicketsDto> liticketsDtosPa = new List<TicketsDto>();
                        List<Tickets> liticketsPa = TicketsForPa.ToList();
                        foreach (var item in liticketsPa)
                        {
                            TicketsDto ticketsDto = new TicketsDto();
                            ticketsDto.User = new UsersDato();
                            ticketsDto.Projects = new ProjectsDto();
                            ticketsDto.Id = item.Id;
                            ticketsDto.ProjectsId = item.ProjectsId;
                            ticketsDto.UserId = item.UserId;
                            ticketsDto.Descriptions = item.Descriptions;
                            ticketsDto.Status = item.Status;
                            ticketsDto.User.Name = item.User.Name;
                            ticketsDto.User.Password = item.User.Password;
                            ticketsDto.User.Phone = item.User.Phone;
                            ticketsDto.User.Id = item.User.Id;
                            ticketsDto.Projects.Name = item.Projects.Name;
                            ticketsDto.Projects.Id = item.Projects.Id;
                            ticketsDto.AssgintoId = item.AssgintoId;
                            liticketsDtosPa.Add(ticketsDto);
                        }
                        return liticketsDtosPa;
                    }

                    else if (employee.department.Name == "Dev")
                    {
                        var projectForEmpolyes = repositoryPM.LoadAll().Where(p => p.UserId == userId).Select(P => P.ProjectsId).ToList();
                        var TIcketsForDev = from t1 in repository.LoadAll()
                                            join pe in repositoryPM.LoadAll()
                                            on t1.ProjectsId equals pe.ProjectsId
                                            where pe.UserId == userId && (t1.Status == Status.waitingforDev.ToString() || (t1.AssgintoId == userId && t1.Status == Status.Pendeing.ToString()))
                                            select t1;
                        List<TicketsDto> liticketsDtosDev = new List<TicketsDto>();
                        List<Tickets> liticketsDev = TIcketsForDev.ToList();
                        foreach (var item in liticketsDev)
                        {
                            TicketsDto ticketsDto = new TicketsDto();
                            ticketsDto.User = new UsersDato();
                            ticketsDto.Projects = new ProjectsDto();
                            ticketsDto.Id = item.Id;
                            ticketsDto.ProjectsId = item.ProjectsId;
                            ticketsDto.UserId = item.UserId;
                            ticketsDto.Descriptions = item.Descriptions;
                            ticketsDto.Status = item.Status;
                            ticketsDto.User.Name = item.User.Name;
                            ticketsDto.User.Password = item.User.Password;
                            ticketsDto.User.Phone = item.User.Phone;
                            ticketsDto.User.Id = item.User.Id;
                            ticketsDto.Projects.Name = item.Projects.Name;
                            ticketsDto.Projects.Id = item.Projects.Id;
                            ticketsDto.AssgintoId = item.AssgintoId;
                            liticketsDtosDev.Add(ticketsDto);
                        }
                        return liticketsDtosDev;
                    }
                    else if (employee.department.Name == "OManger")
                    {

                        var projectForEmpolyer = repositoryPM.LoadAll().Where(p => p.UserId == userId).Select(P => P.ProjectsId).ToList();
                        var TicketsForOManger = from t1 in repository.LoadAll()
                                                join pe in repositoryPM.LoadAll()
                                                on t1.ProjectsId equals pe.ProjectsId
                                                where pe.UserId == userId
                                                select t1;

                        List<TicketsDto> liticketsDtosOM = new List<TicketsDto>();
                        List<Tickets> liticketsOM = TicketsForOManger.ToList();
                        foreach (var item in liticketsOM)
                        {
                            TicketsDto ticketsDto = new TicketsDto();
                            ticketsDto.User = new UsersDato();
                            ticketsDto.Projects = new ProjectsDto();
                            ticketsDto.Id = item.Id;
                            ticketsDto.ProjectsId = item.ProjectsId;
                            ticketsDto.UserId = item.UserId;
                            ticketsDto.Descriptions = item.Descriptions;
                            ticketsDto.Status = item.Status;
                            ticketsDto.User.Name = item.User.Name;
                            ticketsDto.User.Password = item.User.Password;
                            ticketsDto.User.Phone = item.User.Phone;
                            ticketsDto.User.Id = item.User.Id;
                            ticketsDto.Projects.Name = item.Projects.Name;
                            ticketsDto.Projects.Id = item.Projects.Id;
                            ticketsDto.Esc = item.Esc;
                            liticketsDtosOM.Add(ticketsDto);
                        }
                        return liticketsDtosOM;
                    }
                }
                else
                {
                    List<Tickets> liticketsElse = tickets.Where(x => x.UserId == userId).ToList();
                    List<TicketsDto> liticketsDtosElse = new List<TicketsDto>();
                    foreach (var item in liticketsElse)
                    {
                        TicketsDto ticketsDto = new TicketsDto();
                        ticketsDto.Id = item.Id;
                        ticketsDto.ProjectsId = item.ProjectsId;
                        ticketsDto.UserId = item.UserId;
                        ticketsDto.Descriptions = item.Descriptions;
                        ticketsDto.Status = item.Status;
                        ticketsDto.User.Name = item.User.Name;
                        ticketsDto.User.Password = item.User.Password;
                        ticketsDto.User.Phone = item.User.Phone;
                        ticketsDto.User.Id = item.User.Id;
                        ticketsDto.Projects.Name = item.Projects.Name;
                        ticketsDto.Projects.Id = item.Projects.Id;

                        liticketsDtosElse.Add(ticketsDto);
                    }
                    return liticketsDtosElse;

                }
            }
            else if (client != null)
            {

                List<Tickets> liticketsClient = tickets.Where(x => x.UserId == userId).ToList();
                List<TicketsDto> liticketsDtosclient = new List<TicketsDto>();

                foreach (var item in liticketsClient)
                {
                    TicketsDto ticketsDto = new TicketsDto();

                    ticketsDto.User = new UsersDato();
                    ticketsDto.User.Role = new UserRoleDto();
                    ticketsDto.Projects = new ProjectsDto();
                    ticketsDto.Id = item.Id;
                    ticketsDto.ProjectsId = item.ProjectsId;
                    ticketsDto.UserId = item.UserId;
                    ticketsDto.Descriptions = item.Descriptions;
                    ticketsDto.Status = item.Status;
                    ticketsDto.User.Name = item.User.Name;
                    ticketsDto.User.Password = item.User.Password;
                    ticketsDto.User.Phone = item.User.Phone;
                    ticketsDto.User.Id = item.User.Id;
                    ticketsDto.Projects.Name = item.Projects.Name;
                    ticketsDto.Projects.Id = item.Projects.Id;
                    ticketsDto.Esc = item.Esc;

                    liticketsDtosclient.Add(ticketsDto);
                }
                return liticketsDtosclient;
            }
            List<Tickets> liticketsDefult = tickets.ToList();
            List<TicketsDto> liticketsDtosDefult = new List<TicketsDto>();

            foreach (var item in liticketsDtosDefult)
            {
                TicketsDto ticketsDto = new TicketsDto();
                ticketsDto.User = new UsersDato();
                ticketsDto.Projects = new ProjectsDto();
                ticketsDto.Id = item.Id;
                ticketsDto.ProjectsId = item.ProjectsId;
                ticketsDto.UserId = item.UserId;
                ticketsDto.Descriptions = item.Descriptions;
                ticketsDto.Status = item.Status;
                ticketsDto.User.Name = item.User.Name;
                ticketsDto.User.Password = item.User.Password;
                ticketsDto.User.Phone = item.User.Phone;
                ticketsDto.User.Id = item.User.Id;
                ticketsDto.Projects.Name = item.Projects.Name;
                ticketsDto.Projects.Id = item.Projects.Id;
                liticketsDtosDefult.Add(ticketsDto);
            }
            return liticketsDtosDefult;

        }


        public void Done(TicketsDto ticketsDto, int Id, int userId)
        {
            var UserRoleId = userService.LoadAll().Where(u => u.Id == userId).Select(u => u.RoleId).ToList();
            Employee employee = new Employee();
            employee = repositoryEmpolyee.GetAllEmp().FirstOrDefault(x => x.UserId == userId);

            if (employee.department.Name == "pa")
            {
                var xx = (from t1 in repository.LoadAll() where t1.Id == Id select t1).SingleOrDefault();
                xx.Status = Status.waitingforDev.ToString();
                repository.Update(xx);
            }
            else if (employee.department.Name == "Dev")
            {
                var xx = (from t1 in repository.LoadAll() where t1.Id == Id select t1).SingleOrDefault();
                xx.Status = Status.Done.ToString();
                repository.Update(xx);

            }

        }
        public void Pull(TicketsDto ticketsDto, int userId, int Id)
        {

            var UserRoleId = userService.LoadAll().Where(u => u.Id == userId).Select(u => u.RoleId).ToList();

            Employee employee = new Employee();
            employee = repositoryEmpolyee.GetALLD().FirstOrDefault(x => x.UserId == userId);

            if (employee.department.Name == "pa")
            {
                var projectForEmpolye = repositoryPM.LoadAll().Where(p => p.UserId == userId).Select(P => P.ProjectsId).ToList();
                var xx = (from t1 in repository.LoadAll() where t1.Id == Id select t1).SingleOrDefault();
                xx.Status = Status.Pendeing.ToString();
                xx.AssgintoId = userId;
                repository.Update(xx);
            }

            else if (employee.department.Name == "Dev")
            {
                var xx = (from t1 in repository.LoadAll() where t1.Id == Id select t1).SingleOrDefault();
                xx.Status = Status.Pendeing.ToString();
                xx.AssgintoId = userId;
                repository.Update(xx);
            }
        }

        public void urgent(HistoryDto historyDto, int ProjectsId, string ProjectName)
        {

            historyDto.Date = DateTime.Now;
            historyDto.ProjectsId = ProjectsId;
            historyDto.IsUrgent = true;
            historyDto.Status = "WaitingToOpration";


            History history = new History();
            history.Date = DateTime.Now;
            history.ProjectsId = ProjectsId;
            history.Name = ProjectName;
            history.IsUrgent = true;
            history.Description = historyDto.Description;
            history.Status = "WaitingToOpration";
            repositoryhistory.Insert(history);
        }

        public void Reject(TicketsDto ticketsDto, int Id)
        {
            var Tickets = (from t1 in repository.LoadAll() where t1.Id == Id select t1).SingleOrDefault();
            Tickets.Status = Status.reject.ToString();
            repository.Update(Tickets);

        }
        public void escalation(HistoryDto historyDto, int Id)
        {
            Tickets tickets = repository.Load(Id);
            HistoryDto Hs = new HistoryDto();
            Hs.Projects = new ProjectsDto();
            Hs.Projects.Name = tickets.Projects.Name;
            Hs.Date = DateTime.Now;
            Hs.ProjectsId = tickets.ProjectsId;
            Hs.Description = historyDto.Description;
            Hs.Status = "WaitingToOpration";
            History history = new History();
            history.Projects = new Projects();
            history.Date = DateTime.Now;
            history.ProjectsId = tickets.ProjectsId;
            history.Projects.Name = tickets.Projects.Name;
            history.Description = historyDto.Description;
            history.Status = "WaitingToOpration";
            history.Name = tickets.User.Name;
            repositoryhistory.Insert(history);
            var Tickets = (from t1 in repository.LoadAll() where t1.Id == Id select t1).SingleOrDefault();
            Tickets.Esc = true;
            repository.Update(Tickets);
        }

        public void EscalationToLeader(HistoryDto historyDto, int Id)
        {
            History history = repositoryhistory.Load(Id);
            history.Id = historyDto.Id;
            history.Description = historyDto.Description;
            history.Status = "waitingforLeader";
            history.EscLeader = true;
            history.ProjectsId = history.ProjectsId;
            repositoryhistory.Update(history);
        }
    }
}