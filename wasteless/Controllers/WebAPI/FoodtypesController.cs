using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using wasteless.EntityModel;
using wasteless.Services;

namespace wasteless.Controllers.WebAPI
{
    public class FoodtypesController : ApiController
    {
        // GET: api/Foodtypes
        public IHttpActionResult Get()
        {
            IHttpActionResult response = ResponseMessage(new HttpResponseMessage(HttpStatusCode.NotImplemented));
            return response;
        }

        // GET: api/Foodtypes/5
        public List<FoodType> Get(string query, string options)
        {
            var searchResult = DBService.GetFoodTypesSearchResult(query, options);
            return searchResult;
        }

        // POST: api/Foodtypes
        public void Post([FromBody]FoodTypePostDTO foodTypePostDTO)
        {
            DBService.CreateFoodType(foodTypePostDTO.createfoodtypename, foodTypePostDTO.createfoodtypecode);
        }

        // PUT: api/Foodtypes/5
        public IHttpActionResult Put(int id, [FromBody]string value)
        {
            IHttpActionResult response = ResponseMessage(new HttpResponseMessage(HttpStatusCode.NotImplemented));
            return response;
        }

        // DELETE: api/Foodtypes/5
        public void Delete(int id)
        {
            DBService.DeleteFoodType(id.ToString());
        }

        public class FoodTypePostDTO
        {
            public string createfoodtypename { get; set; }
            public string createfoodtypecode { get; set; }
        }
    }
}
