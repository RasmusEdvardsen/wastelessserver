using System;
using System.Web;
using System.Web.Mvc;
using wasteless.EntityModel;
using wasteless.Forms;
using wasteless.Services;

namespace wasteless.Controllers
{
    public class HomeController : Controller
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public ActionResult Home()
        {
            //TODO: MOVE INLINED CSS TO Site.css INSTEAD!
            //TODO: CONSIDER AUTH ISLOGGEDIN ELSEWHERE (LIKE GLOBAL.ASAX)
            //TODO: IMPLEMENT LOGOUT FUNCTIONALITY
            //TODO: GIVE BOXES ACTIONLINKS!
            //TODO: IF PATH NOT CONTAINS "HOME", PUT MENUS IN NAVBAR
            try
            {
                ViewBag.IsLoggedIn = false;
                if (AuthService.IsLoggedIn(HttpContext.Request.Cookies))
                    ViewBag.IsLoggedIn = true;
                return View();
            }
            catch (Exception e)
            {
                log.Error(e.ToString());
                return PartialView("~/Views/Shared/Error.cshtml");
            }
        }

        [HttpPost]
        public PartialViewResult Login(LoginForm loginForm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (DBService.FormLogin(loginForm))
                    {
                        User userToLogin = DBService.GetUser(loginForm.Email);
                        var cookie = new HttpCookie("wstlssusr", userToLogin.ident.ToString()) { Expires = DateTime.Now.AddMinutes(10) };
                        HttpContext.Response.Cookies.Add(cookie);
                        return PartialView("~/Views/Home/_Menu.cshtml", true);
                    }
                }
                catch (Exception ex)
                {
                    log.Error(ex.ToString());
                    return PartialView("~/Views/Shared/Error.cshtml");
                }
            }
            else
            {
                return PartialView("~/Views/Shared/Error.cshtml");
            }
            return PartialView("~/Views/Shared/Error.cshtml");
        }
    }
}