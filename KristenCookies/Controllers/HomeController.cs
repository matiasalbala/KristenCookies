using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KristenCookies.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Session["IsAdmin"] == null) Session["IsAdmin"] = false;
            return View();
        }

        public ActionResult Login(string inputUsername, string inputPass)
        {

            //This is a super basic LOGIN for this example app, I get the credentials from Web config, 
            //this mus be made with something more strong like using JWT, Domain authentication or something similar
            
            string _adminUsername, _adminPass;

            if(!String.IsNullOrEmpty(inputUsername.Trim()) && !String.IsNullOrEmpty(inputPass.Trim()))
            {
                _adminUsername = System.Configuration.ConfigurationManager.AppSettings["admin-username"];
                _adminPass = System.Configuration.ConfigurationManager.AppSettings["admin-password"];

                Session["IsAdmin"] = false;

                if (inputUsername.Equals(_adminUsername) && inputPass.Equals(_adminPass))
                {
                    Session["IsAdmin"] = true;
                }
            }
            else
            {
                Session["IsAdmin"] = false;
            }

            return RedirectToAction("Index");
        }

        public ActionResult Logout()
        {

            Session["IsAdmin"] = false;

            return RedirectToAction("Index");
        }


    }
}