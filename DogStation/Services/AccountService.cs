using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using DogStation.DAO;
using DogStation.Models;
using log4net;
using DogStation.Utils;

namespace DogStation.Services
{
    public class AccountService
    {
        private static readonly DogLoverRepository repository = new DogLoverRepository(new dog_stationEntities());
        private static readonly ILog logger = LogManager.GetLogger("myLog");

        public string GenerateToken(string username, string password, long userId)
        {
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(0, username, DateTime.Now,
                DateTime.Now.AddDays(3), true, string.Format("{0}&{1}&{2}", userId, username, password),
                FormsAuthentication.FormsCookiePath);
            var token = FormsAuthentication.Encrypt(ticket);
            HttpContext.Current.Session[username] = token;
            return token;
        }

        public bool GenerateCookie(string token)
        {
            if (HttpContext.Current.Request.Cookies["Token"] != null)
            {
                logger.Debug("Cookie[Token] exists");
                HttpContext.Current.Request.Cookies.Remove("Token");
                //return false;
            }
            HttpCookie cookie = new HttpCookie("Token");
            cookie.Domain = HttpContext.Current.Request.Url.Host;
            cookie.Path = "/";
            cookie.Value = token;
            HttpContext.Current.Response.AppendCookie(cookie);
            return true;
        }

        public MyStatusCode CleanSessionAndCookie(string username)
        {
            MyStatusCode state = MyStatusCode.Validated;
            if (username == null)
                state = MyStatusCode.Invalid;
            else
            {
                HttpContext.Current.Session.Remove(username);
                HttpContext.Current.Request.Cookies.Remove("Token");
            }
            return state;
        }

        public MyStatusCode ValidateAccount(string username, string password, ref long userId)
        {
            MyStatusCode state = MyStatusCode.Invalid;
            if (!CheckAccountInfo(username, password))
                return state;
            string pw = repository.GetPw(username);

            if (pw == null)
            {
                logger.Debug("no such user : " + username);
                state = MyStatusCode.NoUser;
            }
            else if (!password.Equals(pw))
            {
                logger.Debug(string.Format("wrong pw : {0} of {1}", pw, username));
                state = MyStatusCode.WrongPw;
            }
            else
            {
                userId = repository.GetId(username);
                state = MyStatusCode.Validated;
            }

            return state;
        }

        public MyStatusCode RegisterAccount(string username, string password, string gender)
        {
            MyStatusCode state = MyStatusCode.Invalid;
            if (!CheckAccountInfo(username, password) ||
                !(gender.Equals("M") || gender.Equals("F")))
                return state;
            if (repository.GetPw(username) != null)
            {
                logger.Debug("existing name :" + username);
                state = MyStatusCode.Existing;
            }
            else
            {
                DogLover lover = new DogLover()
                {
                    name = username,
                    password = password,
                    gender = gender,
                    figure = DefaultUtil.DefaultLoverFigure,
                    loves = 0,
                    loveDogs = 0,
                    adoptDogs = 0
                };
                repository.Add(lover);
                state = MyStatusCode.Validated;
            }
            return state;
        }

        private bool CheckAccountInfo(string name, string pw)
        {
            return !(string.IsNullOrEmpty(name) || string.IsNullOrEmpty(pw)
                || name.Count() > 20 || pw.Count() > 30);
        }
    }
}