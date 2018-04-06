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

        [HttpPost]
        public ActionResult Login(LoginForm loginForm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (DBService.FormLogin(loginForm))
                        return View("~/Views/FoodType/FoodType.cshtml", ListableFoodTypeResolver.GetListableFoodTypeResolver());
                }
                catch (Exception ex)
                {
                    log.Error(ex.ToString());
                    return View("~/Views/Shared/Error.cshtml");
                }
            }
            else
            {
                return View("~/Views/Shared/Error.cshtml");
            }
            return View("~/Views/Shared/Error.cshtml");
        }
    }
}