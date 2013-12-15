using BPCCScheduler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BPCCScheduler.Controllers
{
    
    public class HomeController : Controller
    {

        const string LOGIN_SESSION_KEY = "bpcc_admin_login";

        [RequireHttps]
        public ActionResult Index()
        {
            if (!IsAuthorized(null))
            {
                return View("Login");
            }

            return View();
            //return Redirect("content/bpcc-scheduler.html");
        }

        [HttpGet]
        [RequireHttps]
        public ActionResult Login()
        {
            return View("Login");
        }

        [HttpPost]
        public ActionResult Login(LoginModel login)
        {
            if (!IsAuthorized(login))
            {
                return View("Login");
            }
            return View("Index");
        }

        public ActionResult Logout()
        {
            Session[LOGIN_SESSION_KEY] = null;

            return View("Login");
 
        }

        /// <summary>
        /// Simplest thing that will work for the moment until I figure out proper authentication
        /// </summary>
        public bool IsAuthorized(LoginModel login)
        {
            //if the login is passed in, then set the session, if null, then get login from session
            

            if (login != null)
            {
                Session[LOGIN_SESSION_KEY] = login;
            }
            else
            {
                login = Session[LOGIN_SESSION_KEY] as LoginModel;
                if (login == null)
                {
                    return false;
                }
            }

            var users = new List<LoginModel>();
            users.Add(new LoginModel() { UserName = "Jeanette Elwell", Password = "5700070" });
            users.Add(new LoginModel() { UserName = "Lynn Hoy", Password = "9379647" });
            users.Add(new LoginModel() { UserName = "Lewis James", Password = "8635922" });
            users.Add(new LoginModel() { UserName = "Cathy McGreevy", Password = "8304155" });
            users.Add(new LoginModel() { UserName = "Linda Taber", Password = "6685497" });
            users.Add(new LoginModel() { UserName = "Pam Warner", Password = "6014780" });
            users.Add(new LoginModel() { UserName = "Fanny White", Password = "3480877" });
            users.Add(new LoginModel() { UserName = "Jean York", Password = "5425639" });
            users.Add(new LoginModel() { UserName = "Denis Kitchen", Password = "5146236" });

            var user = users.Where(u =>
                u.UserName.Equals(login.UserName, StringComparison.OrdinalIgnoreCase)
                && u.Password.Equals(login.Password))
                .SingleOrDefault();

            return user != null;

        }


    }
}
