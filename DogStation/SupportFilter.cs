using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Security;

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
            var ticket = FormsAuthentication.Decrypt(encrypted).UserData;
            var index = ticket.Split('&');
            string username = index[1];
            string password = index[2];

            var str = HttpContext.Current.Session[username];
            return str == null ? false : encrypted.Equals(str.ToString());
        }

        public static long GetUserIdFromCookie()
        {
            long userId = 0;
            try
            {
                HttpCookieCollection cookies = HttpContext.Current.Request.Cookies;
                HttpCookie cookie = cookies.Get("Token");
                var ticket = FormsAuthentication.Decrypt(cookie.Value).UserData;
                string[] info = ticket.Split('&');
                userId = Convert.ToInt64(info[0]);
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
                var ticket = FormsAuthentication.Decrypt(cookie.Value).UserData;
                string[] info = ticket.Split('&');
                username = info[1];
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