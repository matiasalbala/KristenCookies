using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KristenCookies
{
    public class BaseSecuredController:Controller
    {
        public ActionResult ValidateSession()
        {
            bool _isLogedIn = false;

            _isLogedIn = !(Session["IsAdmin"] == null)? (bool)Session["IsAdmin"]: false ;

            if (!_isLogedIn)
            {
                Response.StatusCode = 401;
                Response.Redirect(Url.Action("Index", "Home"));
            }

            return View();

        }
    }
}