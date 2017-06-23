using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DogStation.Repository;
using DogStation.Entity.Models;
using log4net;
using DogStation.Utils;
using DogStationCLIB;
using Microsoft.Practices.Unity;
using DogStationComLib;
using System.Net.Http.Headers;

namespace DogStation.Services
{
    public class AccountService
    {
        [Dependency]
        public DogLoverRepository loverDao { get; set; }

        private static readonly ILog logger = LogManager.GetLogger("myLog");

        public string GenerateToken(string username, string password, long userId)
        {
            ComClassClass com = new ComClassClass();
            int result = 0;
            com.add(1, 2, ref result);

            var token = CppTokenHolder.GenToken((int)userId, username, password);
            HttpContext.Current.Session[username] = token;
            return token;
        }

        public bool GenerateCookie(string token)
        {
            //if (HttpContext.Current.Request.Cookies["Token"] != null)
            //{
            //    logger.Debug("Cookie[Token] exists");
            //    HttpContext.Current.Request.Cookies.Remove("Token");
            //    //return false;
            //}
            var cookie = new HttpCookie("Token", token);
            cookie.Domain = HttpContext.Current.Request.Url.Host;
            cookie.Path = "/";
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
            if (!AccountUtil.CheckAccountInfo(username, password))
                return state;
            string pw = loverDao.GetPw(username);

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
                userId = loverDao.GetId(username);
                state = MyStatusCode.Validated;
            }

            return state;
        }

        public MyStatusCode RegisterAccount(string username, string password, string gender)
        {
            MyStatusCode state = MyStatusCode.Invalid;
            if (!AccountUtil.CheckAccountInfo(username, password) ||
                !(gender.Equals("M") || gender.Equals("F")))
                return state;
            if (loverDao.GetPw(username) != null)
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
                loverDao.Add(lover);
                state = MyStatusCode.Validated;
            }
            return state;
        }

    }
}