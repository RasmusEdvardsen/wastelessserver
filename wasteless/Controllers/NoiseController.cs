using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using wasteless.Resolvers;
using wasteless.Services;

namespace wasteless.Controllers
{
    public class NoiseController : Controller
    {
        // GET: Noise
        public ActionResult Noise()
        {
            if (AuthService.IsLoggedIn(HttpContext.Request.Cookies))
                return View("~/Views/Noise/Noise.cshtml", NoiseListResolver.GetNoiseListResolver());
            return RedirectToAction("Home", "Home");
        }
    }
}