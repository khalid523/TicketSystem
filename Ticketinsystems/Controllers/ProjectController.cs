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
    public class ProjectController : Controller
    {
   
        private readonly IProjectService projectService;
        private readonly IPmService pmService;
        public ProjectController(IProjectService _projectService, IPmService _pmService )
        {
            this.projectService = _projectService;
            pmService = _pmService;
           
        }
        // GET: Project
        //[Authorize(Roles = "LoadProject")]
        //public ActionResult Index()
        //{
        //    List<ProjectsDto> li = new List<ProjectsDto>();
        //    li = projectService.Load();
        //    return View("Index", li);
        //}
        [Authorize(Roles = "LoadProject")]
        public ActionResult Index1()
        {

            return View();
        }
        [Authorize(Roles = "LoadProject")]
        public ActionResult List()
        {
            return Json(projectService.Load(), JsonRequestBehavior.AllowGet);
            //return View("List", DepartmentService.Load());
        }
        public ActionResult Create()
        {
            return View();
        }
        [Authorize(Roles = "addProject")]
        [HttpPost]
        public ActionResult Create(ProjectsDto projectsDto)
        {
            projectService.Insert(projectsDto);
            ViewBag.Message = String.Format("Project Has add");
            List<ProjectsDto> li = new List<ProjectsDto>();
            li = projectService.Load();
          
            return Json(li, JsonRequestBehavior.AllowGet);
        }
       [Authorize(Roles = "DeleteProject")]
        public ActionResult Delete(int Id)
        {
            projectService.Delete(Id);
            List<ProjectsDto> li = new List<ProjectsDto>();
            li = projectService.Load();
            return Json(li,JsonRequestBehavior.AllowGet);
            //return View("Index", li);
        }

      
        public ActionResult Edit(int Id)
        {
            ProjectsDto projectsDto =  projectService.load1(Id);
            var ProjetForEmployee = projectService.LoadAll().Where(c => c.ProjectsId == Id).Select(x => x.User).ToList();

            TempData["Pro"]= ProjetForEmployee;
            ViewBag.EmpolyeeName =new MultiSelectList(ProjetForEmployee, "Id", "Name");
            //TempData["Idd"] = projectsDto.Id;
            //Session["id"] = Id;
            return Json(projectsDto, JsonRequestBehavior.AllowGet);
            //return View("Edit", projectsDto);
        }
        public ActionResult getProject(int Id)
        {
            var D = projectService.LoadAll().Where(c => c.ProjectsId == Id).ToList();
            return Json(projectService.LoadAll().Where(c => c.ProjectsId == Id).Select(x => x.User).ToList().Select(p => new
            {
                Id = p.Id,
                Name = p.Name,
            }).ToList(), JsonRequestBehavior.AllowGet) ;
        }

        //[Authorize(Roles = "EditProject")]
        //[HttpPost]
        //public ActionResult Edit(ProjectsDto projectsDto, int[] EmployeeName)
        //{
        //    projectService.update(projectsDto, EmployeeName);
        //   return RedirectToAction("Index");
        //}

        [Authorize(Roles = "EditProject")]
        [HttpPost]
        public ActionResult Update(ProjectsDto projectsDto, int[] EmployeeName)
        {
            projectService.update(projectsDto);
            List<ProjectsDto> li = new List<ProjectsDto>();
            li = projectService.Load();
            return Json(li, JsonRequestBehavior.AllowGet);
        }
    }
}