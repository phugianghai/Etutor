using Etutor.DAL;
using Etutor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Etutor.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        private EtutorContext db = new EtutorContext();
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
        bool ValidateUser(string username, string password)
        {
            var check = db.Accounts.Where(s => s.Username == username && s.Password == password).FirstOrDefault();
            if (check != null)
                return true;
            return false;
        }
        //[HttpPost]
        public ActionResult Login(string username, string password, bool rememberMe)
        {
            bool result = ValidateUser(username, password);
            if (result)
            {
                FormsAuthentication.SetAuthCookie(username, rememberMe);

                var user = db.Accounts.Where(m => m.Username == username).FirstOrDefault();

                return checkType(user);
            }
            else
            {
                return View("Index");
            }

        }
        public ActionResult checkType(Account type)
        {
            switch (type.Role)
            {
                case 1: return RedirectToAction("Index", "Assign");
                case 2: return RedirectToAction("Index", "Tutor", new { id = type.Id });
                case 3: return RedirectToAction("Index", "Student",new { id=type.Id});
                default: return RedirectToAction("Index", "Home");
            }
        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }
    }

}