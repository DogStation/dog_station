using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DogStation.IRepository;
using DogStation.Repository;
using DogStation.IServices;
using DogStation.Services;


namespace DogStation
{
    public class DependencyRegisterType
    {
        public static UnityContainer Container()
        {
            var container = new UnityContainer();
            container.RegisterType<IActivityRepository, ActivityRepository>();
            container.RegisterType<IDogLoverRepository, DogLoverRepository>();
            container.RegisterType<IDogRepository, DogRepository>();
            container.RegisterType<IDonateRepository, DonateRepository>();
            container.RegisterType<IFollowRepository, FollowRepository>();
            container.RegisterType<IInventoryRepository, InventoryRepository>();

            container.RegisterType<IAccountService, AccountService>();
            container.RegisterType<IDogService, DogService>();
            container.RegisterType<IDogLoverService, DogLoverService>();

            return container;
        }
    }
}