using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DogStation.Entity.Models;
using System.Data.Entity;


namespace DogStation.Repository
{
    public class FollowRepository : IRepository<Follow>
    {
        public readonly RescueDogEntities db = RescueDog.Instance();


        public Follow Add(Follow t)
        {
            db.Entry(t).State = EntityState.Added;
            db.SaveChanges();
            return t;
        }

        public Follow Delete(long idDog, long idLover)
        {
            //删除follow关系
            Follow f = Get(idDog, idLover);
            if (f == null) return null;
            db.Entry(f).State = EntityState.Deleted;
            db.SaveChangesAsync();
            return f;
        }

        public Follow Get(long idDog, long idLover)
        {
            List<Follow> follows = db.Follow.Where(f => f.lover_ == idLover
           && f.dog_ == idDog).ToList();
            return follows.Count() == 0 ? null : follows[0];

        }

        public List<Follow> GetAll()
        {
            throw new NotImplementedException();
        }

        public bool Update(Follow t)
        {
            throw new NotImplementedException();
        }

        public List<Dog> GetFollowDogs(long userId)
        {
            var query = from f in db.Follow
                        where f.lover_ == userId
                        select f.Dog;
            return query.ToList();

        }

        public Follow Get(long id)
        {
            throw new NotImplementedException();
        }

        public Follow Delete(long id)
        {
            throw new NotImplementedException();
        }
    }
}