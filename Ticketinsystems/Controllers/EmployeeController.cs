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
    public class EmployeeController : Controller
    {
        private readonly IEmpolyeeServices empolyeeServices;
        private readonly IUserService userService;
        private readonly IPmService pmService;
        private readonly IProjectService projectService;
        private readonly IUserRole userRole;
        private readonly IDepartmentService departmentService;
        public EmployeeController(IEmpolyeeServices _empolyeeServices, IUserService _userService,IPmService _pmService,IProjectService _projectService,IUserRole _userRole,IDepartmentService _departmentService)
        {
            empolyeeServices = _empolyeeServices;
            userService = _userService;
            pmService = _pmService;
            projectService = _projectService;
            userRole = _userRole;
            departmentService = _departmentService;
        }

        // GET: Employee
        [Authorize(Roles = "LoadEmployee")]
        public ActionResult Index()
        {
        
            List<EmpolyeeDto> li = new List<EmpolyeeDto>();
            li = empolyeeServices.empolyeeDtos();
     
            
            return View("Index", li);
          
        }
       
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(empolyeeServices.empolyeeDtos(), "Id", "Name");
            ViewBag.projectsId = new MultiSelectList(projectService.Load(), "Id", "Name");
            ViewBag.departmentid = new SelectList(departmentService.Load(), "Id", "Name");
            ViewBag.RoleId = new SelectList(userRole.Load(), "RoleId", "RoleName");
            return View();
        }
        [Authorize(Roles = "AddEmployee")]
        [HttpPost]
       public ActionResult Create(EmpolyeeDto empolyeeDto,int [] projectsId ,int RoleId,int departmentid)
        {
            ViewBag.UserId = new SelectList(empolyeeServices.empolyeeDtos(), "Id", "Name");
            ViewBag.projectsId = new MultiSelectList(projectService.Load(), "Id", "Name");
            ViewBag.departmentid = new SelectList(departmentService.Load(), "Id", "Name");
            ViewBag.RoleId = new SelectList(userRole.Load(), "RoleId", "RoleName");
            var useress= empolyeeServices.empolyeeDtos().Where(u=>u.Email == empolyeeDto.Email).FirstOrDefault();
            if (useress == null)
            {
                empolyeeServices.Insert(empolyeeDto, projectsId, RoleId, departmentid);

         

                ViewBag.Message = String.Format("Empolyee Has add");
            }
            else
            {
                ViewBag.Message = String.Format(" the email haas existed");
            }
            List<EmpolyeeDto> li = new List<EmpolyeeDto>();
            li = empolyeeServices.empolyeeDtos();

            return View("Index",li);
        }
        [Authorize(Roles = "DeleteEmployee")]
        [HttpGet]
        public ActionResult Delete(int Id)
        {
            EmpolyeeDto empolyeeDto = empolyeeServices.Edit(Id);
            var userId = Convert.ToInt32(empolyeeDto.UserId);
            List<EmpolyeeDto> li = new List<EmpolyeeDto>();
            empolyeeServices.Delete(Id);
            userService.Delete(userId);
            li = empolyeeServices.empolyeeDtos();

            ViewBag.MessagealretForDelete = String.Format("The Employee Has Deleted");
            return View("Index", li);
        }

        [HttpGet]
        public ActionResult Edit(int Id)
        {
            ViewBag.UserId = new SelectList(empolyeeServices.empolyeeDtos(), "Id", "Name");
            EmpolyeeDto empolyeeDto= empolyeeServices.Edit(Id);
            var projectForEmpolye = pmService.LoadALl().Where(p => p.UserId == empolyeeDto.UserId).Select(P => P.ProjectsId).ToList();
            ViewBag.projectEmp1 = new SelectList(projectService.Load(), "Id", "Name");
            ViewBag.projectEmp = pmService.LoadALl().Where(p => p.UserId == empolyeeDto.UserId).Select(P => P.ProjectsId).ToArray();
            List<EmpolyeeDto> li = new List<EmpolyeeDto>();
            ViewBag.departmentid = new SelectList(departmentService.Load(), "Id", "Name");
            ViewBag.RoleId = new SelectList(userRole.Load(), "RoleId", "RoleName");
            TempData["IDEmp"] = empolyeeDto.UserId;
            return View("Edit", empolyeeDto);
        }
        [Authorize(Roles = "EditEmployee")]
        [HttpPost]
        public ActionResult Edit(EmpolyeeDto empolyeeDto, int[] projectsEmp, int RoleId, int departmentid)
        {
            ViewBag.UserId = new SelectList(empolyeeServices.empolyeeDtos(), "Id", "Name");
            ViewBag.projectEmp1 = new MultiSelectList(projectService.Load(), "Id", "Name");
            ViewBag.departmentid = new SelectList(departmentService.Load(), "Id", "Name");
            ViewBag.RoleId = new SelectList(userRole.Load(), "RoleId", "RoleName");
            empolyeeDto.UserId = Convert.ToInt32(TempData["IDEmp"]);  
            empolyeeServices.update(empolyeeDto, projectsEmp, RoleId, departmentid);
            List<EmpolyeeDto> li = new List<EmpolyeeDto>();
            li = empolyeeServices.empolyeeDtos();
           
            TempData["EmployeeForEdit"] = "Empolyee Has Edited";
            return RedirectToAction("Index");

        }
    }
}