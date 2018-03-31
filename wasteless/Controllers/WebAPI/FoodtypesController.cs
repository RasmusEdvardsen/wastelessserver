using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using wasteless.Constants;

namespace wasteless.Controllers.WebAPI
{
    public class FoodtypesController : ApiController
    {
        // GET: api/Foodtypes
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Foodtypes/5
        public string Get(string query, string options)
        {
            var queryToUse = QueryTemplates.getTemplate(options);

            //DB call here.

            return "value";
        }

        // POST: api/Foodtypes
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Foodtypes/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Foodtypes/5
        public void Delete(int id)
        {
        }
    }
}
