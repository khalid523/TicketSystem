//using System;

//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Security;

//namespace Ticketinsystems.data
//{
//    public class Role : RoleProvider
//    {
        
//        //public int Id { set; get; }
//        public override string ApplicationName { set; get; }

//        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
//        {
//            throw new NotImplementedException();
//        }

//        public override void CreateRole(string roleName)
//        {
//            throw new NotImplementedException();
//        }

//        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
//        {
//            throw new NotImplementedException();
//        }

//        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
//        {
//            throw new NotImplementedException();
//        }

//        public override string[] GetAllRoles()
//        {
//            throw new NotImplementedException();
//        }

//        public override string[] GetRolesForUser(string username)
//        {
//            using (TicketinContext db = new TicketinContext())
//            {
                
//            }
//        }

//        public override string[] GetUsersInRole(string roleName)
//        {
//            throw new NotImplementedException();
//        }

//        public override bool IsUserInRole(string username, string roleName)
//        {
//            throw new NotImplementedException();
//        }

//        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
//        {
//            throw new NotImplementedException();
//        }

//        public override bool RoleExists(string roleName)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}