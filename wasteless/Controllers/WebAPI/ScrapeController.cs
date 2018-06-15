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
            //if (id.Length != 13 && !int.TryParse(id, out int isInt))
            //    return "";

            var barcodesFromDb = DBService.GetEANs().Where(x => x.EAN_Value.ToString() == id).ToList();
            var barcodesToList = barcodesFromDb.OrderByDescending(x => x.EAN_Score).GroupBy(y => y.FoodTypeID).Select(z => z.FirstOrDefault()).ToList();
            
            var list = (ScrapeService.ScrapeGoogle(id) ?? new List<ScrapeService.WordScore>()).ToList();
            list.AddRange(barcodesToList.Select(x => new ScrapeService.WordScore() { WordName = DBService.GetFoodTypeById(x.FoodTypeID ?? default(int)).FoodTypeName, WordCount = x.EAN_Score += 25 }));
            
            return String.Join(", ", list.OrderByDescending(x => x.WordCount).Select(x => x.WordName));
        }

        // POST: api/Scrape
        public IHttpActionResult Post([FromBody]string value)
        {
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
