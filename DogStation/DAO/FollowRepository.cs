using DogStation.DAO.Interfaces;
using DogStation.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DogStation.DAO
{
    public class FollowRepository
    {
        private dog_stationEntities db = null;
        public FollowRepository(DbContext context)
        {
            db = (dog_stationEntities)context;
        }

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
            db.Entry(f).State = EntityState.Deleted;
            db.SaveChangesAsync();
            return f;
        }

        public Follow Get(long idDog, long idLover)
        {
            return db.Follow.Where(f => f.lover_ == idLover 
            && f.dog_ == idDog).SingleOrDefault();
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
    }
}