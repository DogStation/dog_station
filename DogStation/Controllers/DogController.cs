using DogStation.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DogStation.Controllers
{
    [RoutePrefix("api/dogs")]
    public class DogController : ApiController
    {
        static readonly DogService dogService = new DogService();
        [HttpGet, Route("all")]
        public object GetAllAvailableDogs()
        {
            return dogService.GetAvailableDogs();
        }

        [HttpPost, Route("add")]
        public object AddDog(dynamic data)
        {
            Dictionary<String, Object> result = new Dictionary<string, object>();
            Dog dog = new Dog();
            dog.name = data.name;
            dog.kind = data.kind;
            dog.gender = data.gender;
            dog.figure = data.figure;
            dog.sender = data.sender;
            dog.sendTime = DateTime.Now;
            result.Add("id", dogService.AddDog(dog));
            return result;
        }
    }
}
