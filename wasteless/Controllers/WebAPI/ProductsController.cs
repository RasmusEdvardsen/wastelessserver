using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using wasteless.EntityModel;
using Newtonsoft.Json;
using wasteless.Services;
using System.Reflection;

namespace wasteless.Controllers.WebAPI
{
    public class ProductsController : ApiController
    {
        // GET: api/Product
        public IHttpActionResult Get()
        {
            IHttpActionResult response = ResponseMessage(new HttpResponseMessage(HttpStatusCode.NotImplemented));
            return response;
        }

        // GET: api/Product/5
        public IHttpActionResult Get(int userID) //TODO:?concreteproducts=true
        {
            var products = DBService.GetProductsForUser(userID);
            return products.Any() 
                ? ResponseMessage(new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(JsonConvert.SerializeObject(products)) })
                : ResponseMessage(new HttpResponseMessage(HttpStatusCode.InternalServerError));
        }

        // POST: api/Product
        public IHttpActionResult Post([FromBody]ProductDTO productDTO)
        {
            var httpRspMsg = new HttpResponseMessage(HttpStatusCode.InternalServerError);
            var ean = DBService.CreateEAN(productDTO.EAN, productDTO.FoodTypeName);
            if(ean != null)
            {
                var product = DBService.CreateProduct(new Product() { UserID = productDTO.UserID, EANID = ean.EANID, ExpirationDate = productDTO.ExpirationDate });
                if (ean != null && product)
                    httpRspMsg.StatusCode = HttpStatusCode.OK;
            }
            IHttpActionResult response = ResponseMessage(httpRspMsg);
            return response;
        }

        // PUT: api/Product/5
        public IHttpActionResult Put(int id, [FromBody]string value)
        {
            IHttpActionResult response = ResponseMessage(new HttpResponseMessage(HttpStatusCode.NotImplemented));
            return response;
        }

        // DELETE: api/Product/5
        public IHttpActionResult Delete(int id)
        {
            IHttpActionResult response = ResponseMessage(new HttpResponseMessage(HttpStatusCode.NotImplemented));
            return response;
        }
        
    }

    public class ProductDTO
    {
        public int UserID { get; set; }
        public int EAN { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string FoodTypeName { get; set; }
    }
}
