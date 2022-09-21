using businesslogic.Dto;
using businesslogic.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ticketinsystems.data;

namespace Ticketinsystems.Controllers
{
    public class TicketsController : Controller
    {

        private readonly ITicketService ticketService;
        private readonly IEmpolyeeServices empolyeeServices;
        private readonly IUserService userService;
        private readonly IProjectService projectService;
        private readonly IPmService pmService;
        private readonly IClientService clientService;
        private readonly IHistoryService historyService;
        public TicketsController(ITicketService _ticketService,IEmpolyeeServices _empolyeeServices,IUserService _userService,IProjectService _projectService, IPmService _pmService,IClientService _clientService,IHistoryService _historyService)
        {
            ticketService = _ticketService;
            empolyeeServices = _empolyeeServices;
            userService = _userService;
            projectService = _projectService;
            pmService = _pmService;
            clientService = _clientService;
            historyService = _historyService;
        }
        // GET: Tickets

        [HttpPost]
        public ActionResult ReadCookie()
        {
            var nameCookie = Request.Cookies.AllKeys.ToList();
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "LoadTicket")]
        public ActionResult Index()
        {
            int? userId = null;
            if (Request.Cookies["UserId"].Value != null)
            {
                userId = Convert.ToInt32(Request.Cookies["UserId"].Value);
            }
            ticketService.ticketsDtos(userId);
            return View("Index", ticketService.ticketsDtos(userId));

        }
        public ActionResult History()
        {
            int? userId = null;
            if (Request.Cookies["UserId"].Value != null)
            {
                userId = Convert.ToInt32(Request.Cookies["UserId"].Value);
            }
            var ladertrue = pmService.LoadALLL().Where(p => p.UserId == userId).Select(p => p.IsLader).FirstOrDefault();

            if (ladertrue == true)
            {
                return View("History", historyService.LoadAll().Where(h => h.Status == "waitingforLeader").ToList());
            }
            else
            {
                return View("History", historyService.LoadAll().Where(h => h.Status == "WaitingToOpration" || h.Status == "waitingforLeader").ToList());
            }

        }
        [Authorize(Roles = "GoToCreate")]
        [HttpGet]
        public ActionResult Create()
        {
            int? userId = null;
            if (Request.Cookies["UserId"].Value != null)
            {
                userId = Convert.ToInt32(Request.Cookies["UserId"].Value);
            }

            ViewBag.UserId = new SelectList(userService.LoadAll(), "Id", "Name");
            ViewBag.ProjectsId = new SelectList(pmService.LoadALLL().Where(a => a.UserId == userId).Select(b => b.Projects ).ToList(), "Id", "Name");

            return View();
        }
        [HttpGet]
        public ActionResult urgent()
        {
            int? userId = null;
            if (Request.Cookies["UserId"].Value != null)
            {
                userId = Convert.ToInt32(Request.Cookies["UserId"].Value);
            }

            ViewBag.UserId = new SelectList(userService.LoadAll(), "Id", "Name");
            ViewBag.ProjectsId = new SelectList(pmService.LoadALLL().Where(a => a.UserId == userId).Select(b => b.Projects).ToList(), "Id", "Name");
            return View("urgent");
        }

        [HttpGet]
        public ActionResult EscalationToLeader(int Id)
        {
            TempData["IdEscLeader"] = Id;

            return View();
        }

        [HttpPost]
        public ActionResult EscalationToLeader(HistoryDto historyDto,int Id)
        {
            var IdEscLeader = TempData["IdEscLeader"];
            ticketService.EscalationToLeader(historyDto, Id);
            historyService.LoadAll();
            return View( "History",historyService.LoadAll());
        }
        [HttpPost]
        public ActionResult urgent(HistoryDto historyDto, int ProjectsId)
        {
            int? userId = null;
            if (Request.Cookies["UserId"].Value != null)
            {
                userId = Convert.ToInt32(Request.Cookies["UserId"].Value);
            }
            ViewBag.UserId = new SelectList(userService.LoadAll(), "Id", "Name");
            ViewBag.ProjectsId = new SelectList(pmService.LoadALLL().Where(a => a.UserId == userId).Select(b => b.Projects).ToList(), "Id", "Name");
            var ProjectId = clientService.ClientDtos().Where(u => u.UserId == userId).Select(u => u.Name).FirstOrDefault();
            var ProjectName = projectService.Load().Where(p => p.Id == ProjectsId).Select(p => p.Name).FirstOrDefault();
            ViewBag.MessagealretForurgent = String.Format("The urgent Has Added");
            ticketService.urgent(historyDto, ProjectsId, ProjectName);
            return View("urgent");

        }

        [Authorize(Roles = "addTicket")]
        [HttpPost]
        public ActionResult Create(TicketsDto ticketsDto, int ProjectsId)
        {
            int userId = Convert.ToInt32(Request.Cookies["UserId"].Value);
            if (Request.Cookies["UserId"].Value != null)
            {
                ticketService.Insert(ticketsDto, ProjectsId, userId);
                
            }

            ViewBag.Message = String.Format("Tickets Has add");
            return RedirectToAction("Index");


        }
        [Authorize(Roles = "EditTicket")]
        [HttpPost]
        public ActionResult Edit(TicketsDto  ticketsDto)
        {
            return View();
        }


        [HttpGet]
        public ActionResult Done(TicketsDto ticketsDto , int Id)
        {
            int userId = Convert.ToInt32(Request.Cookies["UserId"].Value);
            if (Request.Cookies["UserId"].Value != null)
            {
            }
            ViewBag.UserId = userId;
            ViewBag.ProjectsId = new SelectList(projectService.Load(), "Id", "Name");
            ticketService.Done(ticketsDto, Id, userId);
            return RedirectToAction("Index");

        }
        [HttpGet]
        public ActionResult Reject(TicketsDto ticketsDto, int Id)
        {
            ViewBag.UserId = new SelectList(userService.LoadAll(), "Id", "Name");
            ViewBag.ProjectsId = new SelectList(projectService.Load(), "Id", "Name");

            int? userId = null;
            if (Request.Cookies["UserId"].Value != null)
            {
                userId = Convert.ToInt32(Request.Cookies["UserId"].Value);
            }
            ticketService.Reject(ticketsDto, Id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult pullnow( int Id)
        {
            TicketsDto ticketsDto = new TicketsDto();
            int userId = Convert.ToInt32(Request.Cookies["UserId"].Value);
            if (Request.Cookies["UserId"].Value != null)
            {
            }
            ViewBag.userId = userId;
            ViewBag.ProjectsId = new SelectList(projectService.Load(), "Id", "Name");
            ticketService.Pull(ticketsDto, userId, Id);
   
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult escalation(int Id)
        {
            TempData["escalation"] = Id;
            return View();
        }

        [HttpPost]
        public ActionResult escalation(HistoryDto historyDto)
        {
            var escalationId = (int)TempData["escalation"];
            ticketService.escalation(historyDto, escalationId);
            return RedirectToAction("Index");

        }

    }
}