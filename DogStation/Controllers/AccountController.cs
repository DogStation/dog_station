using DogStation.Services;
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
        [HttpGet, Route("login")]
        public object Login(string username, string password)
        {
            Dictionary<String, Object> result = new Dictionary<string, object>();

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
