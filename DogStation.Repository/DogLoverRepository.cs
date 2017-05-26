using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DogStation.Entity.Models;
using System.Data.Entity;

namespace DogStation.Repository
{
    public class DogLoverRepository : IRepository<DogLover>
    {
        public readonly RescueDogEntities db = RescueDog.Instance();


        public DogLover Add(DogLover t)
        {
            db.Entry(t).State = EntityState.Added;
            db.SaveChanges();
            return t;
        }

        public DogLover Delete(long id)
        {
            throw new NotImplementedException();
        }

        public DogLover Get(long id)
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

        public long GetId(string username)
        {
            DogLover lover = GetUserByName(username);
            return lover == null ? 0 : lover.idUser;
        }

        public string GetPw(string username)
        {
            DogLover lover = GetUserByName(username);
            return lover == null ? null : lover.password;
        }

        private DogLover GetUserByName(string username)
        {
            DogLover lover = db.DogLover.Where(d => d.name == username).SingleOrDefault();
            return lover;
        }

        public void IncLoves(long loverId, int inc)
        {
            DogLover lover = Get(loverId);
            lover.loves += inc;
            db.Entry(lover).Property(l => l.loves).IsModified = true;
            db.SaveChangesAsync();
        }

        public void AdoptDog(DogLover lover, int dec)
        {
            db.DogLover.Attach(lover);
            lover.loves -= dec;
            lover.adoptDogs += 1;
            db.Entry(lover).Property(l => l.loves).IsModified = true;
            db.Entry(lover).Property(l => l.adoptDogs).IsModified = true;
            db.SaveChangesAsync();
        }

        public void UpdateFollow(DogLover lover, int c)
        {
            db.DogLover.Attach(lover);
            lover.loveDogs += c;
            db.Entry(lover).Property(l => l.loveDogs).IsModified = true;
            db.SaveChangesAsync();
        }

        
    }
}