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
        private static readonly string connString = ConfigurationManager.ConnectionStrings["wastelessDB"].ConnectionString;
        // GET: Login
        public ActionResult Home()
        {
            //SqlConnection conn = new SqlConnection(connString);
            //conn.Open();
            return View();
        }
    }
}