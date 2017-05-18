using DogStation.DAO.Interfaces;
using DogStation.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;


namespace DogStation.DAO
{
    public class DogLoverRepository : IRepository<DogLover>
    {
        private dog_stationEntities db = null;
        public DogLoverRepository(DbContext context)
        {
            db = (dog_stationEntities)context;
        }

        public DogLover Add(DogLover t)
        {
            db.Entry(t).State = EntityState.Added;
            db.SaveChanges();
            return t;
        }

        public DogLover Delete(int id)
        {
            throw new NotImplementedException();
        }

        public DogLover Get(int id)
        {
            return db.DogLover.Find(id);
        }

        public List<DogLover> GetAll()
        {
            return db.DogLover.ToList();
        }

        public bool Update(DogLover t)
        {
            db.Entry(t).State = EntityState.Modified;
            db.SaveChangesAsync();
            return true;
        }

        public string GetPw(string username)
        {
            var query = from lover in db.DogLover
                        where lover.name == username
                        select lover;
            return query.ToList().FirstOrDefault().password;
        }
    }
}