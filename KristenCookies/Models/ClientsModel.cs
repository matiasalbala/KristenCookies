using KristenCookies.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KristenCookies.Models
{
    public class ClientsModel:BaseModel 
    {

        public override void Add(BaseEntity entity)
        {
            Clients _client = (Clients)entity;

            _client.CreationDate = DateTime.Now;
            //Extra validations here

            this.DbContext.Clients.Add(_client);
            this.DbContext.SaveChanges();
        }

       
        public void Save()
        {
            this.DbContext.SaveChanges();
        }

        public override void Update(BaseEntity entity)
        {
            throw new NotImplementedException();
        }

        public override void Delete(BaseEntity entity)
        {
            Clients _client = (Clients)entity;

            var _orderSearch = (from a in this.DbContext.Orders
                                where a.Id == _client.Id
                                select a).FirstOrDefault();

            if (_orderSearch != null)
            {
                this.DbContext.Clients.Remove(_client);
                this.DbContext.SaveChanges();
            }
        }

        public List<Clients> GetList()
        {
            var _clients = from a in this.DbContext.Clients
                          select a;

            //Just for a quick example I return the complete List, but this is not performant, 
            //the best practice is paginate it
            return _clients.ToList();
        }

        public Clients Get(int id)
        {
            var _clientSearch = (from a in this.DbContext.Clients
                                where a.Id == id
                                select a).FirstOrDefault();

            return _clientSearch;
        }

        public Clients GetByEmail(string email)
        {
            var _clientSearch = (from a in this.DbContext.Clients
                                 where a.Email == email
                                 select a).FirstOrDefault();

            return _clientSearch;
        }

    }
}