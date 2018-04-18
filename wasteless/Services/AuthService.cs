using System;
using System.Web;
using wasteless.EntityModel;

namespace wasteless.Services
{
    public class AuthService
    {
        public static bool IsLoggedIn(HttpCookieCollection httpCookieCollection)
        {
            if(httpCookieCollection["wstlssusr"] != null)
            {
                if (Guid.TryParse(httpCookieCollection["wstlssusr"].Value, out Guid guid))
                {
                    User user = DBService.GetUser(guid);
                    return user != null ? true : false;
                }
            }
            return false;
        }
    }
}