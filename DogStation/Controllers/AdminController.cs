using DogStation.Entity.Models;
using DogStation.Services.Service;
using DogStation.Utils;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DogStation.Controllers
{
    [RoutePrefix("api/admins")]
    public class AdminController : ApiController
    {
        [Dependency]
        public AdminService adminService { get; set; }

        [HttpPost, Route("activity")]
        public HttpResponseMessage PlayActivity(dynamic data)
        {
            HttpResponseMessage message = new HttpResponseMessage();
            Activity activity = new Activity()
            {
                idActivity = 0,
                kind = data.kind,
                desc = data.desc,
                images = data.images,
                admins = " " + data.admins,
                lovers = " " + data.lovers,
                dogs = " " + data.dogs
            };
            MyStatusCode state = adminService.PlayActivity(activity);
            message.StatusCode = (HttpStatusCode)state;
            return message;
        }
    }
}
