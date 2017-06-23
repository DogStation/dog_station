using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Security;
using DogStationCLIB;

namespace DogStation
{
    public class SupportFilter : AuthorizeAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            HttpCookieCollection cookies = HttpContext.Current.Request.Cookies;
            HttpCookie cookie = cookies.Get("Token");
            if (cookie != null)
            {
                var token = cookie.Value;
                if (!string.IsNullOrEmpty(token))
                {
                    if (ValidateTicket(token))
                    {
                        base.IsAuthorized(actionContext);
                        return;
                    }
                }
            }
            HandleUnauthorizedRequest(actionContext);
        }

        private bool ValidateTicket(string encrypted)
        {
            var username = CppTokenHolder.ExtractName(encrypted);
            //var str = HttpContext.Current.Session[username];
            //return str == null ? false : encrypted.Equals(str.ToString());
            return username != null;
        }

        public static long GetUserIdFromCookie()
        {
            long userId = 0;
            try
            {
                HttpCookieCollection cookies = HttpContext.Current.Request.Cookies;
                HttpCookie cookie = cookies.Get("Token");
                userId = CppTokenHolder.ExtractId(cookie.Value);
            }
            catch (NullReferenceException)
            {

            }
            catch (Exception)
            {

            }
            return userId;
        }

        public static string GetUsernameFromCookie()
        {
            string username = null;
            try
            {
                HttpCookieCollection cookies = HttpContext.Current.Request.Cookies;
                HttpCookie cookie = cookies.Get("Token");
                username = CppTokenHolder.ExtractName(cookie.Value);
            }
            catch (NullReferenceException)
            {

            }
            catch (Exception)
            {

            }
            return username;
        }
    }
}