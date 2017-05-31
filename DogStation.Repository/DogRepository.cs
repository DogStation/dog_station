using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DogStation.Entity.Models;
using System.Data.Entity;
using DogStation.Utils;

namespace DogStation.Repository
{
    public class DogRepository : IRepository<Dog>
    {
        public readonly RescueDogEntities db = RescueDog.Instance();


        public Dog Add(Dog t)
        {
            db.Entry(t).State = EntityState.Added;
            db.SaveChanges();
            return t;
        }

        public Dog Delete(long id)
        {
            throw new NotImplementedException();
        }

        public Dog Get(long id)
        {
            return db.Dog.Find(id);
        }

        public List<Dog> GetAll()
        {
            List<Dog> dogs = db.Dog.ToList();
            return dogs;
        }

        public List<Dog> GetAll(long[] ids)
        {
            return db.Dog
                .Where(d => ids.Contains(d.idDog))
                .ToList();
        }

        public void IncLoves(long idDog, int inc)
        {
            Dog dog = Get(idDog);
            dog.loves += inc;
            db.Entry(dog).Property(d => d.loves).IsModified = true;
            db.SaveChangesAsync();
        }

        public List<Dog> GetFreeDogs(int page)
        {
            return db.Dog
                .Where(d => d.adopter == 0)
                .OrderByDescending(d => d.sendTime)
                .Skip(DefaultUtil.DefaultDogPageSize * (page - 1))
                .Take(DefaultUtil.DefaultDogPageSize)
                .ToList();
        }

        public List<Dog> GetSentDogs(long userId)
        {
            return db.Dog
                .Where(d => d.sender == userId)
                .OrderByDescending(d => d.sendTime)
                .ToList();
        }

        public List<Dog> GetAdoptedDogs(long userId)
        {
            return db.Dog
                .Where(d => d.adopter == userId)
                .OrderByDescending(d => d.adoptTime)
                .ToList();
        }

        

        public bool Update(Dog t)
        {
            db.Entry(t).State = EntityState.Modified;
            db.SaveChangesAsync();
            return true;
        }

        public void BeAdopted(Dog dog, long userId)
        {
            db.Dog.Attach(dog);
            dog.adopter = userId;
            dog.adoptTime = DateTime.Now;
            db.Entry(dog).Property(d => d.adopter).IsModified = true;
            db.Entry(dog).Property(d => d.adoptTime).IsModified = true;
            db.SaveChangesAsync();
        }
    }
}