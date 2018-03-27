using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using wasteless.Forms;

namespace wasteless.Controllers
{
    public class LoginController : Controller
    {
        [HttpPost]
        public ActionResult Login(LoginForm loginForm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //DBMVCService.Login
                    return View();
                }
                catch (Exception ex)
                {
                    return View("~/Views/Shared/Error.cshtml");
                }
            }
            else
            {
                return View("~/Views/Shared/Error.cshtml");
            }
        }
    }
}