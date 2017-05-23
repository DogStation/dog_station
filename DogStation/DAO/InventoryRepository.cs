using DogStation.DAO.Interfaces;
using DogStation.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DogStation.DAO
{
    public class InventoryRepository : IRepository<Inventory>
    {
        private dog_stationEntities db = null;
        public InventoryRepository(DbContext context)
        {
            db = (dog_stationEntities)context;
        }

        public Inventory Add(Inventory t)
        {
            throw new NotImplementedException();
        }

        public Inventory Delete(long id)
        {
            throw new NotImplementedException();
        }

        public Inventory Get(long id)
        {
            throw new NotImplementedException();
        }

        public List<Inventory> GetAll()
        {
            return db.Inventory.ToList();
        }

        public bool Update(Inventory t)
        {
            throw new NotImplementedException();
        }
    }
}