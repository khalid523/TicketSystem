using businesslogic.Dto;
using businesslogic.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

using Ticketinsystems.data;

namespace Ticketinsystems.Controllers
{
    public class ClientController : Controller
    {
        private readonly IClientService clientService;
        private readonly IUserService userService;
        private readonly IPmService pmService;
        private readonly IProjectService projectService;
        private readonly IUserRole userRole;
        private readonly IDepartmentService departmentService;
        public ClientController(IClientService _clientService, IUserService _userService, IPmService _pmService, IProjectService _projectService, IUserRole _userRole, IDepartmentService _departmentService)
        {
            clientService = _clientService;
            userService = _userService;
            pmService = _pmService;
            projectService = _projectService;
            userRole = _userRole;
            departmentService = _departmentService;
        }
        // GET: Client
        [Authorize(Roles = "LoadClient")]
        public ActionResult Index()
        {
            List<ClientDto> li = new List<ClientDto>();
            li = clientService.ClientDtos();
            return View("Index", li);
        }
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(clientService.ClientDtos(), "Id", "Name");
            ViewBag.projectsId = new MultiSelectList(projectService.Load(), "Id", "Name");
            ViewBag.RoleId = new SelectList(userRole.Load(), "RoleId", "RoleName");

            return View();
        }
        [Authorize(Roles = "CreateClient")]
        [HttpPost]
        public ActionResult Create(ClientDto clientDto, int[] projectsId ,int RoleId)
        {
            ViewBag.UserId = new SelectList(clientService.ClientDtos(), "Id", "Name");
            ViewBag.projectsId = new MultiSelectList(projectService.Load(), "Id", "Name");
            ViewBag.RoleId = new SelectList(userRole.Load(), "RoleId", "RoleName");
            var useress = clientService.ClientDtos().Where(u => u.Email == clientDto.Email).FirstOrDefault();
            if (useress == null)
            {
                clientService.Insert(clientDto, projectsId, RoleId);
            }
            else
            {
                ViewBag.Messagealret = String.Format("the email haas existed");
            }
            List<ClientDto> li = new List<ClientDto>();
            li = clientService.ClientDtos();
            ViewBag.MessagealretClient = String.Format("The Client Has Added");
            return View("Index", li);
        }
        [Authorize(Roles = "DeleteClient")]
        [HttpGet]
        public ActionResult Delete(int Id)
        {
            ClientDto empolyeeDto = clientService.Edit(Id);
            var userId = Convert.ToInt32(empolyeeDto.UserId);
            List<ClientDto> li = new List<ClientDto>();
            clientService.Delete(Id);
            userService.Delete(userId);
            li = clientService.ClientDtos();
            ViewBag.MessagealretForDelete = String.Format("The Client Has Deleted");
            return View("Index", li);
        }
        [HttpGet]
       public ActionResult Edit(int Id)
         {
            ViewBag.UserId = new SelectList(clientService.ClientDtos(), "Id", "Name");
            ClientDto clientDto = clientService.Edit(Id);
            var projectForEmpolye = pmService.LoadALl().Where(p => p.UserId == clientDto.UserId).Select(P => P.ProjectsId).ToList();
            ViewBag.projectEmp1 = new MultiSelectList(projectService.Load(), "Id", "Name");
            ViewBag.projectEmp = pmService.LoadALl().Where(p => p.UserId == clientDto.UserId).Select(P => P.ProjectsId).ToArray();
            ViewBag.RoleId = new SelectList(userRole.Load(), "RoleId", "RoleName");
            TempData["IdUser"] = clientDto.UserId;
            return View("Edit", clientDto);
        }
        [Authorize(Roles = "EditClient")]
        [HttpPost]
        public ActionResult Edit(ClientDto clientDto, int[] projectsEmp, int RoleId)
        {
            ViewBag.UserId = new SelectList(clientService.ClientDtos(), "Id", "Name");
            ViewBag.projectEmp1 = new MultiSelectList(projectService.Load(), "Id", "Name");
            ViewBag.RoleId = new SelectList(userRole.Load(), "RoleId", "RoleName");
            clientDto.UserId =  Convert.ToInt32(TempData["IdUser"]);
            clientService.update(clientDto, projectsEmp, RoleId);
            TempData["ClientForEdit"] = "the Client has Edited";
            return  RedirectToAction("Index");
        }
    }
}