using businesslogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Ticketinsystems.data;

namespace Ticketinsystems
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //using (TicketinContext db = new TicketinContext())
            //{
            //    if (db.userRoles.Count()==0)
            //    {
            //        var role1 = new UserRole();
            //        var role2 = new UserRole();
            //        var role3 = new UserRole();
            //        var role4 = new UserRole();
            //        role1.RoleName = "User";
            //        role2.RoleName = "Admin";
            //        role3.RoleName = "Dev";
            //        role4.RoleName = "pa";
            //        db.userRoles.Add(role1);
            //        db.userRoles.Add(role2);
            //        db.userRoles.Add(role3);
            //        db.userRoles.Add(role4);
            //        db.SaveChanges();

            //    }

            //}
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            UnityConfig.RegisterComponents();
        }
    }
}
