using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using wasteless.Models.DataTransferObjects;
using wasteless.Services;

namespace wasteless.Controllers.WebAPI
{
    public class UsersController : ApiController
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // GET: api/Users
        public IHttpActionResult Get()
        {
            IHttpActionResult response = ResponseMessage(new HttpResponseMessage(HttpStatusCode.NotImplemented));
            return response;
        }

        // GET: api/Users/5
        public IHttpActionResult Get(string email, string password)
        {
            UserConcreteDto user = new UserConcreteDto(DBService.GetUser(email, password));
            
            var httpStatusCode = user.HasRequiredValues ? HttpStatusCode.OK : HttpStatusCode.NotFound;
            var content = user.HasRequiredValues ? JsonConvert.SerializeObject(user) : "";

            var rspMsg = new HttpResponseMessage()
            {
                StatusCode = httpStatusCode,
                Content = new StringContent(content, Encoding.UTF8, "application/json")
            };
            
            IHttpActionResult response = ResponseMessage(rspMsg);
            return response;
        }

        // POST: api/Users
        public IHttpActionResult Post([FromBody]UserPostDTO userPostDTO)
        {
            var rspMsg = new HttpResponseMessage(HttpStatusCode.NotAcceptable);
            if (ModelState.IsValid)
            {
                UserConcreteDto user = new UserConcreteDto(DBService.ClientSignup(userPostDTO.email, userPostDTO.password));
                if (user != null)
                {
                    rspMsg.StatusCode = HttpStatusCode.OK;
                    rspMsg.Content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                }
            }
            IHttpActionResult response = ResponseMessage(rspMsg);
            return response;
        }

        // PUT: api/Users/5
        public IHttpActionResult Put(int id, [FromBody]string value)
        {
            IHttpActionResult response = ResponseMessage(new HttpResponseMessage(HttpStatusCode.NotImplemented));
            return response;
        }

        // DELETE: api/Users/5
        public IHttpActionResult Delete(int id)
        {
            IHttpActionResult response = ResponseMessage(new HttpResponseMessage(HttpStatusCode.NotImplemented));
            return response;
        }

        public class UserPostDTO
        {
            [EmailAddress]
            public string email { get; set; }
            public string password { get; set; }
        }
    }
}
