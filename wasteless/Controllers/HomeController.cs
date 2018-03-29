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
            return View();
        }
    }
}