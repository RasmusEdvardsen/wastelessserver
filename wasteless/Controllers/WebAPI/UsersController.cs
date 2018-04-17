using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using wasteless.Services;

namespace wasteless.Controllers.WebAPI
{
    public class UsersController : ApiController
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // GET: api/Users
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Users/5
        public IHttpActionResult Get(string email, string password)
        {
            var user = DBService.GetUser(email, password);

            var httpStatusCode = user != null ? HttpStatusCode.OK : HttpStatusCode.NotFound;
            var content = user != null ? JsonConvert.SerializeObject(user) : "";

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
            //this.Validate<UserPostDTO>(userPostDTO);
            if (ModelState.IsValid)
                if (DBService.ClientSignup(userPostDTO.email, userPostDTO.password))
                    rspMsg.StatusCode = HttpStatusCode.OK;
            IHttpActionResult response = ResponseMessage(rspMsg);
            return response;
        }

        // PUT: api/Users/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Users/5
        public void Delete(int id)
        {
        }

        public class UserPostDTO
        {
            [EmailAddress]
            public string email { get; set; }
            public string password { get; set; }
        }
    }
}
