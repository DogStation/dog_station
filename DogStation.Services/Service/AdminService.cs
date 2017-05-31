using DogStation.Entity.Models;
using DogStation.Repository;
using DogStation.Utils;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DogStation.Services.Service
{
    public class AdminService
    {
        [Dependency]
        public ActivityRepository activityDao { get; set; }
        [Dependency]
        public DogRepository dogDao { get; set; }
        [Dependency]
        public DogLoverRepository loverDao { get; set; }

        public MyStatusCode PlayActivity(Activity activity)
        {
            MyStatusCode state = MyStatusCode.Validated;
            long[] idDogs = ConverterUtil.ConvertIds(activity.dogs);
            long[] idLovers = ConverterUtil.ConvertIds(activity.lovers);

            using (var dbTransaction = activityDao.db.Database.BeginTransaction())
            {
                try
                {
                    activityDao.Add(activity);
                    foreach(long idLover in idLovers)
                    {
                        loverDao.IncLoves(idLover, DefaultUtil.PlayActivityLoversPoint);
                    }
                    foreach(long idDog in idDogs)
                    {
                        dogDao.IncLoves(idDog, DefaultUtil.PlayActivityLoversPoint);
                    }
                    dbTransaction.Commit();
                }
                catch (Exception)
                {
                    state = MyStatusCode.InternalServerError;
                    dbTransaction.Rollback();
                }
            }

            return state;
        }
    }
}