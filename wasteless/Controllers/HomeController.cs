using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace wasteless.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Home()
        {
            //TODO: Put Login here, and return partial that contains button to redirect to foodtypemvc. makes more sense.
            //TODO: When successful login, give squares with options in them [Food Types] [Logout], etc...
            return View();
        }
    }
}