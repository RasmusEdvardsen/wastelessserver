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
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public ActionResult Home()
        {
            //TODO: Put Login here, and return partial that contains button to redirect to foodtypemvc. makes more sense.
            //TODO: When successful login, give squares with options in them [Food Types] [Logout], etc...
            return View();
        }
    }
}