using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KristenCookies.Controllers
{
    public class ProductsController : BaseSecuredController
    {
        Models.ProductsModel model = new Models.ProductsModel();

        // GET: Products
        public ActionResult Index()
        {
            this.ValidateSession(); //Implements a simple validation functions that Inherits from BaseController

            return View();
        }

        public ActionResult Add()
        {
            this.ValidateSession(); //Implements a simple validation functions that Inherits from BaseController

            return View();
        }

        public ActionResult List()
        {
            this.ValidateSession(); //Implements a simple validation functions that Inherits from BaseController

            ViewBag.Products = this.model.GetList();

            return View();
        }


        public ActionResult AddProduct(string productDescription)
        {
            this.ValidateSession(); //Implements a simple validation functions that Inherits from BaseController

            if (string.IsNullOrEmpty(productDescription.Trim()))
                throw new Exception("You must add a description");

            var _existingProduct = this.model.GetByDescription(productDescription);

            if (_existingProduct != null)
                throw new Exception("The product exists");

            Products _product = new Products();

            _product.Description = productDescription;

            this.model.Add(_product);

            return RedirectToAction("List");
        }
    }
}