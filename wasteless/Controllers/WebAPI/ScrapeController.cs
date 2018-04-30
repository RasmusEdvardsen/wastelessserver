using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using wasteless.Services;

namespace wasteless.Controllers.WebAPI
{
    public class ScrapeController : ApiController
    {
        // GET: api/Scrape
        public IHttpActionResult Get()
        {
            IHttpActionResult response = ResponseMessage(new HttpResponseMessage(HttpStatusCode.NotImplemented));
            return response;
        }

        // GET: api/Scrape/5
        public string Get(string id)
        {
            //TODO: Check if id is of type EAN.
            //test 5741000131077
            //test 5449000080264
            //test 5700426293929
            //TODO: CHECK EAN TABLE IF EXISTS THEN RETURN THAT
            var list = ScrapeService.ScrapeGoogle(id) ?? new List<ScrapeService.WordScore>();
            return String.Join(", ", list.OrderByDescending(x => x.WordCount).Select(x => x.String()));

            //TODO: TRY VISIT OTHER WEBSITES, SEARCH FOR MICRODATA SCHEMAS
            //TODO: digit-eyes.com/upcCode/
            //TODO: http://www.upcchecker.com
        }

        // POST: api/Scrape
        public IHttpActionResult Post([FromBody]string value)
        {
            //TODO: IF USER CONFIRMS FOODTYPE FOR EAN, CREATE RELATION BETWEEN EAN/FOODTYPE IN A TABLE
            IHttpActionResult response = ResponseMessage(new HttpResponseMessage(HttpStatusCode.NotImplemented));
            return response;
        }

        // PUT: api/Scrape/5
        public IHttpActionResult Put(int id, [FromBody]string value)
        {
            IHttpActionResult response = ResponseMessage(new HttpResponseMessage(HttpStatusCode.NotImplemented));
            return response;
        }

        // DELETE: api/Scrape/5
        public IHttpActionResult Delete(int id)
        {
            IHttpActionResult response = ResponseMessage(new HttpResponseMessage(HttpStatusCode.NotImplemented));
            return response;
        }
    }
}
