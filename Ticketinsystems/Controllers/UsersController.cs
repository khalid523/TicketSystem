using businesslogic.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Ticketinsystems.data;


namespace Ticketinsystems.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService userService;
        public UsersController(IUserService _userService)
        {
            userService = _userService;
        }
        // ticktes and project one to many 
        // GET: Users
        [Authorize(Roles = "LoadUser")]
        public ActionResult Index()
        {
            return View(userService.LoadAll());
        }






    }
}