using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DogStation.Entity.Models;
using System.Data.Entity;

namespace DogStation.Repository
{
    public class ActivityRepository : IRepository<Activity>
    {
        public readonly RescueDogEntities db = RescueDog.Instance();


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
