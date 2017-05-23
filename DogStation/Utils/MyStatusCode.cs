using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DogStation.Utils
{
    public enum MyStatusCode
    {
        Validated = 200,
        InternalServerError = 500,
        Invalid = 600,
        NoUser = 601,
        WrongPw = 602,
        Existing = 603,      //注册时用户名已存在
        CannotAfford = 604,  //爱心值不够
        Limited = 605        //收养数量达到上限
    }

}