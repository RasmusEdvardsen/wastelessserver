using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using wasteless.EntityModel;
using wasteless.Services;

namespace wasteless.Controllers.WebAPI
{
    public class NoisesController : ApiController
    {
        // GET: api/Noises
        public IHttpActionResult Get()
        {
            IHttpActionResult response = ResponseMessage(new HttpResponseMessage(HttpStatusCode.NotImplemented));
            return response;
        }

        // GET: api/Noises/5
        public List<Noise> Get(string query, string options)
        {
            var searchResult = DBService.GetNoisesSearchResult(query, options);
            return searchResult;
        }

        // POST: api/Noises
        public void Post([FromBody]NoisePostDTO noisePostDTO)
        {
            DBService.CreateNoise(noisePostDTO.createnoisename);
        }

        // PUT: api/Noises/5
        public IHttpActionResult Put(int id, [FromBody]string value)
        {
            IHttpActionResult response = ResponseMessage(new HttpResponseMessage(HttpStatusCode.NotImplemented));
            return response;
        }

        // DELETE: api/Noises/5
        public void Delete(int id)
        {
            DBService.DeleteNoise(id.ToString());
        }

        public class NoisePostDTO
        {
            public string createnoisename { get; set; }
        }
    }
}
