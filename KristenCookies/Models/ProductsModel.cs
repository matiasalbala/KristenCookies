using KristenCookies.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KristenCookies.Models
{
    public class ProductsModel : BaseModel
    {
        public override void Add(BaseEntity entity)
        {
            Products _product = (Products)entity;

            //Just make a quick search of product, in this case I used Description as example, 
            // but the idea is search by some id or somethig more performant
            var _productsSearch = (from a in this.DbContext.Products
                                  where a.Description.ToLower() == _product.Description.ToLower()
                                  select a);

            if (_productsSearch.Any())
            {
                throw new Exception($"The product \"{ _product.Description}\" exists");
            }

            _product.IdWeb = Guid.NewGuid().ToString().Replace("-", "");

            this.DbContext.Products.Add(_product);
            this.DbContext.SaveChanges();
        }

        public override void Update(BaseEntity entity)
        {
            Products _product = (Products)entity;

            var _productSearch = (from a in this.DbContext.Products
                                  where a.Id == _product.Id
                                  select a).FirstOrDefault();

            if (_productSearch != null)
            {
                _productSearch.Description = _product.Description;
                
                this.DbContext.SaveChanges();
            }
        }

        public override void Delete(BaseEntity entity)
        {
            Products _product = (Products)entity;

            var _productSearch = (from a in this.DbContext.Products
                                  where a.Id == _product.Id
                                  select a).FirstOrDefault();

            if (_productSearch!=null)
            {
                this.DbContext.Products.Remove(_product);
                this.DbContext.SaveChanges();
            }
        }

        public Products Get(int id)
        {
            var _productSearch = (from a in this.DbContext.Products
                                  where a.Id == id
                                  select a).FirstOrDefault();

            return _productSearch;
        }

        public Products GetByDescription(string description)
        {
            var _productSearch = (from a in this.DbContext.Products
                                  where a.Description.ToLower() == description.ToLower()
                                  select a).FirstOrDefault();

            return _productSearch;
        }

        public Products GetByIdWeb(string idweb)
        {
            var _productSearch = (from a in this.DbContext.Products
                                  where a.IdWeb.ToLower() == idweb.ToLower()
                                  select a).FirstOrDefault();

            return _productSearch;
        }


        public List<Products> GetList()
        {

            var _productsList = (from a in this.DbContext.Products
                                  select a);

            return _productsList.ToList();
        }

    }
}