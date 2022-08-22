using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KristenCookies.Controllers
{
    public class OrderController : BaseSecuredController
    {

        Models.ClientsModel _clientsModel = new Models.ClientsModel();
        Models.ProductsModel _productsModel = new Models.ProductsModel();
        Models.OrdersModel _ordersModel = new Models.OrdersModel();


        // GET: Order
        public ActionResult Index()
        {
            Models.ProductsModel _products = new Models.ProductsModel();

            ViewBag.Products = _products.GetList();

            return View();
        }

        public void Add(string clientEmail, List<Models.Entities.OrderItemRequest> cookies)
        {

            Orders _order;
            Clients _client;
            Products _product;
            OrdersDetails _orderItem;

            int _orderCookiesCount = 0;

            _orderCookiesCount = cookies.Sum(x=>x.count);

            if(_orderCookiesCount % 12 != 0)
            {
                throw new Exception("Cannot offer partial dozens of cookies");
            }

            _order = new Orders();
            _client =  _clientsModel.GetByEmail(clientEmail);

            if (_client == null)
            {
                _client = new Clients();
                _client.Email = clientEmail;
                _clientsModel.Add(_client);
                _client = _clientsModel.GetByEmail(clientEmail);
            }

            _order.IdClient = _client.Id;
            
            foreach(Models.Entities.OrderItemRequest _item in cookies) {
                _product = _productsModel.GetByIdWeb(_item.idweb);

                if (_product != null) { 
                    _orderItem = new OrdersDetails();
                    _orderItem.count = _item.count;
                    _orderItem.IdProduct = _product.Id;
                    _ordersModel.AddOrderItem(_orderItem);
                    _order.OrdersDetails.Add(_orderItem);
                }
            }

            _order.IdState = (int) Models.OrdersModel.OrderState.PENDNIG;

            _ordersModel.Add(_order);

        }


        public ActionResult List()
        {
            this.ValidateSession(); //Implements a simple validation functions that Inherits from BaseController

            var _ordersList = this._ordersModel.GetList().OrderBy(x => x.IdState).OrderByDescending(x=>x.CreatedDate);

            ViewBag.Orders = _ordersList;

            return View();
        }

        public ActionResult Complete(int order)
        {
            this.ValidateSession(); //Implements a simple validation functions that Inherits from BaseController

            Orders _order = this._ordersModel.Get(order);

            if (_order != null)
            {
                _order.IdState = (int)Models.OrdersModel.OrderState.COMPLETED;
                _order.CompletedDate = DateTime.Now;
                this._ordersModel.Update(_order);
            }

            return RedirectToAction("List");
        }

        public ActionResult Report()
        {
            this.ValidateSession(); //Implements a simple validation functions that Inherits from BaseController

            //In may case the easy way was use Linq but is better if use an Store Procedure or direct SQL Query

            DateTime _toDate = DateTime.Now.AddDays(-7);
            
            int _pendingOrders = (from a in this._ordersModel.DbContext.Orders
                                 where a.IdState == 1 && a.CreatedDate >= _toDate
                                 select a).Count();

            var mostPopularCookie = (from a in this._ordersModel.DbContext.OrdersDetails
                                        select a).GroupBy(x=>x.Products.Description).Select(n => new
                                        {
                                            MetricName = n.Key,
                                            MetricCount = n.Sum(x=>x.count)
                                        })
                                        .OrderByDescending(n => n.MetricCount).FirstOrDefault();

            var mostPopularHour = (from a in this._ordersModel.DbContext.Orders
                                   select new { Hour = a.CreatedDate.Hour})
                                   .GroupBy(x=>x.Hour)
                                   .OrderByDescending(x=>x.Count())
                                   .FirstOrDefault();

            ViewBag.numberOfOrders = _pendingOrders;
            ViewBag.mostPopularCookie = mostPopularCookie!=null? mostPopularCookie.MetricName:"-";
            ViewBag.mostPopularHour = mostPopularHour!=null? $"{mostPopularHour.Key}:00 Hs":"-";

            return View();
        }
    }
}