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
using System.Web;

namespace DogStation.Controllers
{
    [RoutePrefix("api/lovers")]
    public class DogLoverController : ApiController
    {
        [Dependency]
        public DogLoverService loverService { get; set; }


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


        [SupportFilter]
        [HttpPost, Route("lover/update")]
        public async System.Threading.Tasks.Task<HttpResponseMessage> SeeOwnInfo()
        {
            HttpResponseMessage message = new HttpResponseMessage();
            long userId = SupportFilter.GetUserIdFromCookie();

            if (!Request.Content.IsMimeMultipartContent())
            {
                message.StatusCode = HttpStatusCode.BadRequest;
                return message;
            }

            string root = HttpContext.Current.Server.MapPath("~/App_Data");
            var provider = new MultipartFormDataStreamProvider(root);
            try
            {
                await Request.Content.ReadAsMultipartAsync(provider);
                DogLover lover = loverService.ProcessUpload(provider);
                lover.idUser = userId;
                if (lover.figure != null)
                {
                    message.StatusCode = loverService.Update(lover) ?
                        HttpStatusCode.OK : (HttpStatusCode)MyStatusCode.Invalid;
                }
                else
                {
                    message.StatusCode = HttpStatusCode.InternalServerError;
                }
            }
            catch (Exception e)
            {
                message.StatusCode = HttpStatusCode.InternalServerError;
                return message;
            }


            return message;
        }

        [SupportFilter]
        [HttpPost, Route("lover/comment")]
        public HttpResponseMessage CommentDog(dynamic data)
        {
            HttpResponseMessage message = new HttpResponseMessage();
            long userId = SupportFilter.GetUserIdFromCookie();
            Comment comment = new Comment()
            {
                idComment = 0,
                commenter = userId,
                content = data.content,
                dog = data.dog,
                commentTime = DateTime.Now
            };
            MyStatusCode state = loverService.CommentDog(comment);
            message.StatusCode = (HttpStatusCode)state;
            return message;
        }

        [SupportFilter]
        [HttpGet, Route("comment")]
        public HttpResponseMessage SeeComments(long idDog, int p)
        {
            HttpResponseMessage message = new HttpResponseMessage();
            message.StatusCode = HttpStatusCode.Forbidden;
            if(p > 0)
            {
                List<Dictionary<String, Object>> comments = loverService.SeeComments(idDog, p);
                message.StatusCode = HttpStatusCode.OK;
                string content = LitJson.JsonMapper.ToJson(comments);
                message.Content = new StringContent(content, System.Text.Encoding.UTF8, "application/json");
            }
            return message;
        }
    }
}
