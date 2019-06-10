using Microsoft.AspNet.OData;
using MobWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace MobWebApp.Controllers
{
    [Authorize]
    public class PeopleController : ODataController
    {
        SchoolApp db = new SchoolApp();
        private bool PersonExists(int key)
        {
            return db.People.Any(p => p.ID == key);
        }
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        [EnableQuery]
        public IQueryable<People> Get()
        {
            return db.People;
        }
        [EnableQuery]
        public SingleResult<People> Get([FromODataUri] int key)
        {
            IQueryable<People> result = db.People.Where(p => p.ID == key);
            return SingleResult.Create(result);
        }
    }
}