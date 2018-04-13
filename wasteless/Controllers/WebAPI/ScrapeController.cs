using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using wasteless.Services;

namespace wasteless.Controllers.WebAPI
{
    public class ScrapeController : ApiController
    {
        // GET: api/Scrape
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Scrape/5
        public string Get(string id)
        {
            //test 5741000131077
            //test 5449000080264
            //TODO: CHECK EAN TABLE IF EXISTS THEN RETURN THAT
            var list = ScrapeService.ScrapeGoogle(id) ?? new List<ScrapeService.WordScore>();
            return String.Join(", ", list.OrderByDescending(x => x.WordCount).Select(x => x.String()));

            //TODO: TRY VISIT OTHER WEBSITES, SEARCH FOR MICRODATA SCHEMAS
            //TODO: digit-eyes.com/upcCode/
            //TODO: http://www.upcchecker.com
        }

        // POST: api/Scrape
        public void Post([FromBody]string value)
        {
            //TODO: IF USER CONFIRMS FOODTYPE FOR EAN, CREATE RELATION BETWEEN EAN/FOODTYPE IN A TABLE
        }

        // PUT: api/Scrape/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Scrape/5
        public void Delete(int id)
        {
        }
    }
}
