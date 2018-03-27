using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace wasteless.Controllers
{
    public class HomeController : Controller
    {
        // GET: Login
        public ActionResult Home()
        {
            return View();
        }
    }
}