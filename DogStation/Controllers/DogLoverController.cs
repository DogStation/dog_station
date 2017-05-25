using DogStation.IServices;
using DogStation.Entity.Models;
using DogStation.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Practices.Unity;

namespace DogStation.Controllers
{
    [RoutePrefix("api/lovers")]
    public class DogLoverController : ApiController
    {
        [Dependency]
        public IDogLoverService loverService { get; set; }

        [SupportFilter]
        [HttpPost, Route("lover/donate")]
        public HttpResponseMessage Donate(List<DonateItem> data)
        {
            HttpResponseMessage message = new HttpResponseMessage();
            long userId = SupportFilter.GetUserIdFromCookie();
            MyStatusCode state = loverService.Donate(data, userId);
            message.StatusCode = (HttpStatusCode)state;
            return message;
        }

        //插入到Inventory还没做
        [SupportFilter]
        [HttpGet, Route("donate")]
        public HttpResponseMessage SeeDonations()
        {
            HttpResponseMessage message = new HttpResponseMessage();
            long userId = SupportFilter.GetUserIdFromCookie();
            List<Dictionary<string, object>> donations = loverService.SeeDonations(userId);
            string content = LitJson.JsonMapper.ToJson(donations);
            message.Content = new StringContent(content, System.Text.Encoding.UTF8, "application/json");
            message.StatusCode = HttpStatusCode.OK;
            return message;
        }
    }
}
