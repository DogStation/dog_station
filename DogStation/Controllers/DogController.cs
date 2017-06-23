using DogStation.Services;
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
    [RoutePrefix("api/dogs")]
    public class DogController : ApiController
    {
        [Dependency]
        public DogService dogService { get; set; }

        [SupportFilter]
        [HttpGet, Route("all")]
        public HttpResponseMessage GetAllAvailableDogs(int p)
        {
            long userId = SupportFilter.GetUserIdFromCookie();
            HttpResponseMessage message = new HttpResponseMessage();
            message.StatusCode = HttpStatusCode.Forbidden;
            if (p > 0)
            {
                List<Dictionary<String, Object>> dogs = dogService.GetAvailableDogs(userId, p);
                message.StatusCode = HttpStatusCode.OK;
                string content = LitJson.JsonMapper.ToJson(dogs);
                message.Content = new StringContent(content, System.Text.Encoding.UTF8, "application/json");
            }
            return message;
        }

        [SupportFilter]
        [HttpGet, Route("send")]
        public HttpResponseMessage SeeSentDogs()
        {
            long userId = SupportFilter.GetUserIdFromCookie();
            HttpResponseMessage message = new HttpResponseMessage();
            List<Dictionary<String, Object>> dogs = dogService.GetSentDogs(userId);
            string content = LitJson.JsonMapper.ToJson(dogs);
            message.Content = new StringContent(content, System.Text.Encoding.UTF8, "application/json");
            message.StatusCode = HttpStatusCode.OK;
            return message;
        }


        [SupportFilter]
        [HttpPost, Route("dog/send")]
        public HttpResponseMessage SendDog(dynamic data)
        {
            HttpResponseMessage message = new HttpResponseMessage();
            Dictionary<String, Object> result = new Dictionary<string, object>();
            Dog dog = new Dog()
            {
                name = data.name,
                kind = data.kind,
                gender = data.gender,
                figure = data.figure,
                sender = data.sender,
                sendTime = DateTime.Now,
                adopter = 0
            };

            long idDog = dogService.SendDog(dog);
            result.Add("id", idDog);
            message.StatusCode = idDog == 0 ? HttpStatusCode.InternalServerError : HttpStatusCode.OK;
            string content = LitJson.JsonMapper.ToJson(result);
            message.Content = new StringContent(content, System.Text.Encoding.UTF8, "application/json");
            return message;
        }

        [SupportFilter]
        [HttpPost, Route("dog/adopt")]
        public HttpResponseMessage AdoptDog(dynamic data)
        {
            HttpResponseMessage message = new HttpResponseMessage();
            Dictionary<String, Object> result = new Dictionary<string, object>();
            long idDog = data.idDog;
            long idLover = SupportFilter.GetUserIdFromCookie();
            MyStatusCode state = dogService.AdoptDog(idLover, idDog);
            message.StatusCode = (HttpStatusCode)state;
            return message;
        }

        [SupportFilter]
        [HttpGet, Route("adopt")]
        public HttpResponseMessage SeeAdoptedDogs()
        {
            long userId = SupportFilter.GetUserIdFromCookie();
            HttpResponseMessage message = new HttpResponseMessage();
            List<Dictionary<String, Object>> dogs = dogService.GetAdoptedDogs(userId);
            string content = LitJson.JsonMapper.ToJson(dogs);
            message.Content = new StringContent(content, System.Text.Encoding.UTF8, "application/json");
            message.StatusCode = HttpStatusCode.OK;
            return message;
        }

        [SupportFilter]
        [HttpGet, Route("dog/follow")]
        public HttpResponseMessage FollowDog(long idDog)
        {
            long userId = SupportFilter.GetUserIdFromCookie();
            HttpResponseMessage message = new HttpResponseMessage();
            MyStatusCode state = dogService.FollowDog(userId, idDog);
            message.StatusCode = (HttpStatusCode)state;
            return message;
        }

        [SupportFilter]
        [HttpGet, Route("dog/unfollow")]
        public HttpResponseMessage UnfollowDog(long idDog)
        {
            long userId = SupportFilter.GetUserIdFromCookie();
            HttpResponseMessage message = new HttpResponseMessage();
            MyStatusCode state = dogService.UnfollowDog(userId, idDog);
            message.StatusCode = (HttpStatusCode)state;
            return message;
        }

        [SupportFilter]
        [HttpGet, Route("follow")]
        public HttpResponseMessage SeeFollowDogs()
        {
            long userId = SupportFilter.GetUserIdFromCookie();
            HttpResponseMessage message = new HttpResponseMessage();
            List<Dictionary<String, Object>> dogs = dogService.GetFollowDogs(userId);
            string content = LitJson.JsonMapper.ToJson(dogs);
            message.Content = new StringContent(content, System.Text.Encoding.UTF8, "application/json");
            message.StatusCode = HttpStatusCode.OK;
            return message;
        }

        [SupportFilter]
        [HttpGet, Route("dog/get")]
        public HttpResponseMessage SeeDogBasicInfo(long idDog)
        {
            long userId = SupportFilter.GetUserIdFromCookie();
            HttpResponseMessage message = new HttpResponseMessage();
            Dictionary<string, object> dog = dogService.SeeDogBasic(userId, idDog);
            string content = LitJson.JsonMapper.ToJson(dog);
            message.Content = new StringContent(content, System.Text.Encoding.UTF8, "application/json");
            message.StatusCode = HttpStatusCode.OK;
            return message;
        }

        [SupportFilter]
        [HttpGet, Route("dog/activity")]
        public HttpResponseMessage SeeDogActivities(long idDog)
        {
            HttpResponseMessage message = new HttpResponseMessage();
            List<Dictionary<string, object>> activities = dogService.SeeDogActivities(idDog);
            string content = LitJson.JsonMapper.ToJson(activities);
            message.Content = new StringContent(content, System.Text.Encoding.UTF8, "application/json");
            message.StatusCode = HttpStatusCode.OK;
            return message;
        }
    }
}
