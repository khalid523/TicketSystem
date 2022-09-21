using businesslogic.Dto;
using businesslogic.Services;
using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;



namespace Ticketinsystems.Controllers
{
    public class loginController : Controller
    {
        private  IUserService userService;
        private readonly IHistoryService historyService;
        private readonly IPmService pmService;
        public loginController(IUserService  _userService,IHistoryService _historyService,IPmService _pmService)
        {
            userService = _userService;
            historyService = _historyService;
            pmService = _pmService;
        }
        public ActionResult Create()
        {
            return View();
        }
        public string encrypt(string clearText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }
        public string Decrypt(string cipherText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            var cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }

        [HttpPost]
        public ActionResult Create(LoginDto loginDto)
        {

            loginDto.Password = encrypt(loginDto.Password);
            var myUser = userService.LoadAll()
            .FirstOrDefault(u => u.Email == loginDto.Email
            && u.Password == loginDto.Password &&u.isDelete==false);
            TempData["CountForOpration"] = historyService.LoadAll().Where(h => h.Status == "WaitingToOpration").Count();
            TempData["CountForleader"] = historyService.LoadAll().Where(h => h.Status == "waitingforLeader").Count();


            if (myUser != null )

            {
                var Ticket = new FormsAuthenticationTicket(loginDto.Email, true, 3000);
                string Encrypt =FormsAuthentication.Encrypt(Ticket);
                var cookie=  new HttpCookie(FormsAuthentication.FormsCookieName, Encrypt);
                Session["UserIdSession"] = myUser.Id.ToString();
                var userIdCookie =  new HttpCookie("UserId", myUser.Id.ToString());
                cookie.Expires = DateTime.Now.AddHours(3000);
                cookie.HttpOnly = true;
                Response.Cookies.Add(cookie); 
                Response.Cookies.Add(userIdCookie);

                int? userId = null;
                if (Request.Cookies["UserId"].Value != null)
                {
                    userId = Convert.ToInt32(Request.Cookies["UserId"].Value);
                }
                var IdLaeder = pmService.LoadALLL().Where(p => p.UserId == userId).Select(p => p.IsLader).FirstOrDefault();
                TempData["IDLeader"] = IdLaeder;
         

                if (myUser.RoleId == 2)
                {
                    return RedirectToAction("AdminArea", "Home");
                   
                }

              
                else if(myUser.Id == userId && IdLaeder)
                {

                    TempData["IdladerForPA"] = myUser.Id;
                    return RedirectToAction("UserArea", "Home");
                }
                   else
                {
                    return RedirectToAction("UserArea", "Home");
                }
            }
            return View();
        
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            //if (Request.Cookies["UserId"].Value != null)
            //{
            //    var UserID = new HttpCookie("UserId"); UserID.Expires = DateTime.Now.AddDays(-1); Response.Cookies.Add(UserID);
            //}
            //else
            //{

            //    return RedirectToAction("Create", "login");
            //}
            return RedirectToAction("Create", "login");
        }
    }
}