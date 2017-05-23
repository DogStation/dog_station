using DogStation.DAO.Interfaces;
using DogStation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace DogStation.DAO
{
    public class ActivityRepository : IRepository<Activity>
    {
        private dog_stationEntities db = null;
        public ActivityRepository(DbContext context)
        {
            db = (dog_stationEntities)context;
        }

        public Activity Add(Activity t)
        {
            throw new NotImplementedException();
        }

        public Activity Delete(long id)
        {
            throw new NotImplementedException();
        }

        public Activity Get(long id)
        {
            throw new NotImplementedException();
        }

        public List<Activity> GetActivities(long idDog)
        {
            List<Activity> list = db.Activity.ToList();

            return list;
        }

        public List<Activity> GetAll()
        {
            throw new NotImplementedException();
        }

        public bool Update(Activity t)
        {
            throw new NotImplementedException();
        }
    }
}