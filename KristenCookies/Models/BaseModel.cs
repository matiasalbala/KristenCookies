using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KristenCookies.Models
{
    public abstract class BaseModel
    {
        //All models must inherit of this baseModel that implement the entityModel that make changes on db or get data from db
        //I used EntityFramework just to make this solution more quick, but other option is make a data access layer that save data or get data from db
        //and an entity layer that map the tables on database. Now EntityFramework implement it.
        public Model DbContext = new Model();

        public abstract void Add(Entities.BaseEntity entity);
        public abstract void Update(Entities.BaseEntity entity);
        public abstract void Delete(Entities.BaseEntity entity);

    }
}