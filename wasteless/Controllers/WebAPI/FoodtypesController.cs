using System.Collections.Generic;
using System.Web.Http;
using wasteless.EntityModel;
using wasteless.Services;

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
        public void Put(int id, [FromBody]string value)
        {
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
