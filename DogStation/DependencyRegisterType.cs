using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DogStation.Repository;
using DogStation.Services;
using DogStation.Entity.Models;
using DogStation.Controllers;

namespace DogStation
{
    public class DependencyRegisterType
    {
        public static UnityContainer Container()
        {
            var container = new UnityContainer();

            container.RegisterType<ActivityRepository>();
            container.RegisterType<DogLoverRepository>();
            container.RegisterType<DogRepository>();
            container.RegisterType<DonateRepository>();
            container.RegisterType<FollowRepository>();
            container.RegisterType<InventoryRepository>();

            container.RegisterType<AccountService>();
            container.RegisterType<DogService>();
            container.RegisterType<DogLoverService>();
            container.RegisterType<QiniuService>();
            //container.RegisterType<AccountController>();
            //container.RegisterType<DogController>();
            //container.RegisterType<DogLoverController>();

            return container;
        }
    }
}