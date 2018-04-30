using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using wasteless.Services;

namespace wasteless.Controllers.WebAPI
{
    public class CacheController : ApiController
    {
        // GET: api/Cache
        public IHttpActionResult Get()
        {
            IHttpActionResult response = ResponseMessage(new HttpResponseMessage(HttpStatusCode.NotImplemented));
            return response;
        }

        // GET: api/Cache/5
        public IHttpActionResult Get(string action)
        {
            var httpStatCode = HttpStatusCode.InternalServerError;
            switch (action)
            {
                case "reset":
                    httpStatCode = CacheService.ResetCache() 
                        ? HttpStatusCode.OK 
                        : HttpStatusCode.InternalServerError;
                    break;
                default:
                    httpStatCode = HttpStatusCode.InternalServerError;
                    break;
            }
            IHttpActionResult response = ResponseMessage(new HttpResponseMessage(httpStatCode));
            return response;
        }

        // POST: api/Cache
        public IHttpActionResult Post([FromBody]string value)
        {
            IHttpActionResult response = ResponseMessage(new HttpResponseMessage(HttpStatusCode.NotImplemented));
            return response;
        }

        // PUT: api/Cache/5
        public IHttpActionResult Put(int id, [FromBody]string value)
        {
            IHttpActionResult response = ResponseMessage(new HttpResponseMessage(HttpStatusCode.NotImplemented));
            return response;
        }

        // DELETE: api/Cache/5
        public IHttpActionResult Delete(int id)
        {
            IHttpActionResult response = ResponseMessage(new HttpResponseMessage(HttpStatusCode.NotImplemented));
            return response;
        }
    }
}
