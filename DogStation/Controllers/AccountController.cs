using DogStation.Services;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;


namespace DogStation.Controllers
{
    [RoutePrefix("api/account")]
    public class AccountController : ApiController
    {
        static readonly AccountService accountService = new AccountService();
        static readonly ILog logger = LogManager.GetLogger("myLog");
        [HttpGet, Route("login")]
        public object Login(string username, string password)
        {
            Dictionary<String, Object> result = new Dictionary<string, object>();
            logger.Debug(string.Format("name:{0} pw:{1}", username, password));
            //数据库验证
            bool status = accountService.ValidateAccount(username, password);
            result.Add("status", status);
            if (status)
            {
                //生成token
                string token = accountService.GenerateToken(username, password);
                result.Add("token", token);
            }
            return result;
        }
    }
}
