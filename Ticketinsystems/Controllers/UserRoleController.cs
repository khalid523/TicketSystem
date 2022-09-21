using businesslogic.Dto;
using businesslogic.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Ticketinsystems.data;


namespace Ticketinsystems.Controllers
{
    public class UserRoleController : Controller
    {
        private readonly IUserRole userRole;
        private readonly IpermissionsService ipermissionsService;
        private readonly IPermission_UserRoleService permission_UserRoleService;
        public UserRoleController(IUserRole _userRole,IpermissionsService _ipermissionsService, IPermission_UserRoleService _permission_UserRoleService )
        {
            userRole = _userRole;
            ipermissionsService = _ipermissionsService;
            permission_UserRoleService = _permission_UserRoleService;
        }
        // GET: UserRole
        [Authorize(Roles = "LoadUserRole")]
        public ActionResult Index()
        {
            ViewBag.permissionList = (userRole.Load(), "Id", "Name");
       
            return View("Index", userRole.Load());
        }
        public ActionResult Create()
        {

            ViewBag.permissionList = (ipermissionsService.Load().ToList());
            return View();
        }
        [Authorize(Roles = "CreateUserRole")]
        [HttpPost]
        public ActionResult Create(UserRoleDto userRoleDto,int [] permissionList)
        {
            ViewBag.permissionList = (ipermissionsService.Load().ToList());
            userRole.Insert(userRoleDto, permissionList);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int Id)
        {
            userRole.Delete(Id);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int Id)
        {
            ViewBag.permissionList = (ipermissionsService.Load().ToList());
            ViewBag.permissionListspecific = new MultiSelectList(permission_UserRoleService.Load().Where(i => i.userRoleId == Id).Select(p=>p.Permissions.Name).ToList());
            userRole.Edit(Id);
            return View("Edit", userRole.Edit(Id));
        }
       
        [HttpPost]
        public ActionResult Edit(UserRoleDto userRoleDto, int[] permissionList)
        {
            userRole.update(userRoleDto, permissionList);
            return RedirectToAction("Index");

        }
    }
}
