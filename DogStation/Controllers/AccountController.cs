using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Microsoft.Practices.Unity;
using DogStation.Services;
using DogStation.Utils;

namespace DogStation.Controllers
{
    [RoutePrefix("api/account")]
    public class AccountController : ApiController
    {
        [Dependency]
        public AccountService accountService { get; set; }

        static readonly ILog logger = LogManager.GetLogger("myLog");

        [HttpGet, Route("login")]
        public HttpResponseMessage Login(string username, string password)
        {
            HttpResponseMessage message = Request.CreateResponse();
            Dictionary<String, Object> result = new Dictionary<string, object>();
            logger.Debug(string.Format("name:{0} pw:{1}", username, password));
            //数据库验证
            long userId = 0;
            MyStatusCode state = accountService.ValidateAccount(username, password, ref userId);
            message.StatusCode = (HttpStatusCode)state;
            if (state == MyStatusCode.Validated)
            {
                //生成token
                string token = accountService.GenerateToken(username, password, userId);
                accountService.GenerateCookie(token);
                result.Add("Token", token);
            }
            string content = LitJson.JsonMapper.ToJson(result);
            message.Content = new StringContent(content, System.Text.Encoding.UTF8, "application/json");
            return message;
        }

        [HttpPost, Route("register")]
        public HttpResponseMessage Register(dynamic data)
        {
            HttpResponseMessage message = Request.CreateResponse();
            Dictionary<String, Object> result = new Dictionary<string, object>();
            string username = data.username;
            string password = data.password;
            string gender = data.gender;
            MyStatusCode state = accountService.RegisterAccount(username, password, gender);
            message.StatusCode = (HttpStatusCode)state;
            return message;
        }

        [SupportFilter]
        [HttpGet, Route("logout")]
        public HttpResponseMessage Logout()
        {
            HttpResponseMessage message = Request.CreateResponse();
            string username = SupportFilter.GetUsernameFromCookie();
            MyStatusCode state = accountService.CleanSessionAndCookie(username);
            message.StatusCode = (HttpStatusCode)state;
            return message;
        }
    }

}
