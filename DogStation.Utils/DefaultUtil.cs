using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DogStation.Utils
{
    public class DefaultUtil
    {
        //七牛云上传凭证
        public static readonly string AK = "pMkP9Ra2f0wcNtOY7to-IV8sq7ANoZBQ0y9tUupG";
        public static readonly string SK = "Iu650TkkzTHRyFtPPeSks11IO3uICYajepY93-UO";

        //默认头像和前缀
        public static readonly string DefaultLoverFigure = "http://opbhb1ahv.bkt.clouddn.com/dogs/default_user.jpg";
        public static readonly string DefaultDogFigure = "http://opbhb1ahv.bkt.clouddn.com/dogs/default_dog.jpg";
        public static readonly string DefaultFigurePrefix = "http://opbhb1ahv.bkt.clouddn.com/";

        //个人收养和关注上限
        public static readonly int DefaultAdoptionMaximum = 10;
        public static readonly int DefaultFollowMaximum = 20;
        
        //分页显示每页的上限
        public static readonly int DefaultDogPageSize = 20;
        public static readonly int DefaultCommentPageSize = 10;

        //爱心收益计算
        public static readonly int SentDogLovesPoint = 50;
        public static readonly int PlayActivityLoversPoint = 20;
    }
}