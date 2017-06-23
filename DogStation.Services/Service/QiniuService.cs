using System;
using System.Threading;
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
using DogStation.Services.Service;

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

        public string Filepath { get; private set; }

        public string Savekey { get; private set; }

        public string Token { get; private set; }

        public string Prefix { get; private set; }

        public UploadManager Uploader { get; private set; }

        public QiniuService()
        {
            this.Filepath = null;
            this.Savekey = null;
            this.Token = null;
            this.Uploader = null;
            this.Prefix = "dogs/";
        }

        public string UploadFile(string filepath, string savekey)
        {
            this.Filepath = filepath;
            this.Savekey = savekey;
            this.Token = GenToken();
            this.Uploader = new UploadManager();

            ThreadReturnData trd = new ThreadReturnData();
            trd.reset = new ManualResetEvent(false);
            ThreadPool.QueueUserWorkItem(new WaitCallback(trd.ReturnThreadData), this);
            trd.reset.WaitOne();
            HttpResult result = trd.result;

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