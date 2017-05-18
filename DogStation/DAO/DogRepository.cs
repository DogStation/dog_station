using DogStation.DAO.Interfaces;
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

        public Dog Delete(int id)
        {
            Dog dog = db.Dog.Find(id);
            db.Entry(dog).State = EntityState.Deleted;
            db.SaveChangesAsync();
            return Add(dog);
        }

        public Dog Get(int id)
        {
            Dog dog = db.Dog.Find(id);
            return dog;
        }

        //public Dictionary<String, Object> GetAsDict(int id)
        //{
        //    Dog dog = db.Dog.Find(id);
        //    db.Entry(dog).Reference(d => d.DogLover).Load();

        //    return ConverterUtil.convertDog(dog);
        //}

        public List<Dog> GetAll()
        {
            List<Dog> dogs = db.Dog.ToList();

            return dogs;
        }

        //public List<Dictionary<String, Object>> GetAllAsDict()
        //{
        //    db.Dog.Include(d => d.DogLover);
        //    List<Dog> dogs = db.Dog.ToList();
        //    return ConverterUtil.convertDogs(dogs);
        //}

        public bool Update(Dog t)
        {
            db.Entry(t).State = EntityState.Modified;
            db.SaveChangesAsync();
            return true;
        }


    }
}