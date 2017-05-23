using DogStation.DAO.Interfaces;
using DogStation.Models;
using DogStation.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DogStation.DAO
{
    public class DogRepository : IRepository<Dog>
    {
        private dog_stationEntities db = null;
        public DogRepository(DbContext context)
        {
            db = (dog_stationEntities)context;
        }

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

        public List<Dog> GetFreeDogs()
        {
            return db.Dog
                .Where(d => d.adopter == 0)
                .OrderByDescending(d => d.sendTime)
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