using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ticketinsystems.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "User")]
        public ActionResult UserArea()
        {
           return View();
        }
        [Authorize(Roles = "Admin")]
        public ActionResult AdminArea()
        {


            return View();
        }

        [Authorize(Roles = "Empolyee")]
        public ActionResult EmpoyeeArea()
        {


            return View();
        }

        [Authorize(Roles = "EmployeePull")]
        public ActionResult EmpoyeeArea1()
        {


            return View();
        }
    }
}