using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Qiniu.Util;
using DogStation.Utils;
using System.Web.Http;
using System.Net;
using System.Net.Http;
using Qiniu.Common;
using Qiniu.IO.Model;
using Qiniu.IO;
using Qiniu.Http;
using System.Threading.Tasks;
using LitJson;

namespace DogStation.Services
{
    public class QiniuService
    {
        private static string bucket = "musicradio";
        private static Mac mac = new Mac(DefaultUtil.AK, DefaultUtil.SK);

        static QiniuService()
        {
            Config.SetZone(ZoneID.CN_South, true);
        }


        public string UploadFile(string filepath, string savekey)
        {
            string token = GenToken();

            UploadManager uploader = new UploadManager();
            HttpResult result = uploader.UploadFile(filepath, "dogs/" + savekey, token);
            string figure = null;
            if (result.Code == 200)
            {
                JsonData data = JsonMapper.ToObject(result.Text);
                figure = DefaultUtil.DefaultFigurePrefix + data["key"].ToString();
            }
            return figure;
        }

        private string GenToken()
        {
            PutPolicy putPolicy = new PutPolicy();
            putPolicy.SetExpires(3600);
            putPolicy.Scope = bucket;

            return Auth.CreateUploadToken(mac, putPolicy.ToJsonString());
        }
    }
}