using Qiniu.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace DogStation.Services.Service
{
    public class ThreadReturnData
    {
        public ManualResetEvent reset;

        public HttpResult result;

        public void ReturnThreadData(object state) {
            QiniuService service = state as QiniuService;
            result = service.Uploader.UploadFile(
                service.Filepath, 
                service.Prefix + service.Savekey,
                service.Token);
            reset.Set();
        }
    }
}