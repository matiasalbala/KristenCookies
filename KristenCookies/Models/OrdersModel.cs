using KristenCookies.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KristenCookies.Models
{
    public class OrdersModel:BaseModel
    {

        public enum OrderState
        {
            PENDNIG  = 1,
            COMPLETED = 2
        }

        public override void Add(BaseEntity entity)
        {
            Orders _order = (Orders)entity;

            //Extra validations here
            _order.CreatedDate = DateTime.Now;

            this.DbContext.Orders.Add(_order);
            this.DbContext.SaveChanges();
        }

        public void AddOrderItem(OrdersDetails item)
        {
            this.DbContext.OrdersDetails.Add(item);
        }

        public override void Update(BaseEntity entity)
        {
            this.DbContext.SaveChanges();
        }

        public override void Delete(BaseEntity entity)
        {
            Orders _order = (Orders)entity;

            var _orderSearch = (from a in this.DbContext.Orders
                                  where a.Id == _order.Id
                                  select a).FirstOrDefault();
            
            if (_orderSearch != null)
            {
                this.DbContext.Orders.Remove(_order);
                this.DbContext.SaveChanges();
            }
        }

        public List<Orders> GetList()
        {
            var _orders = from a in this.DbContext.Orders
                          select a;

            //Just for a quick example I return the complete List, but this is not performant, 
            //the best practice is paginate it
            return _orders.ToList();
        }

        public Orders Get(int id)
        {
            var _orderSearch = (from a in this.DbContext.Orders
                                where a.Id == id
                                select a).FirstOrDefault();

            return _orderSearch;
        }

        
    }
}