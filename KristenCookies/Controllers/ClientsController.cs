using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KristenCookies.Controllers
{
    public class ClientsController : BaseSecuredController
    {

        Models.ClientsModel _clientsModel = new Models.ClientsModel();


        // GET: Clients
        public ActionResult Index()
        {
            this.ValidateSession(); //Implements a simple validation functions that Inherits from BaseController

            ViewBag.Clients = _clientsModel.GetList();
            return View();
        }
    }
}