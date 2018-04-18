using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using wasteless.Forms;
using wasteless.Resolvers;
using wasteless.Services;

namespace wasteless.Controllers
{
    public class FoodTypeController : Controller
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        
        public ActionResult FoodType()
        {
            if(AuthService.IsLoggedIn(HttpContext.Request.Cookies))
                return View("~/Views/FoodType/FoodType.cshtml", FoodTypeListResolver.GetFoodTypeListResolver());
            return RedirectToAction("Home", "Home");
        }
    }
}