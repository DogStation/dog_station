﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DogStation.Entity.Models;


namespace DogStation.Repository
{
    public class InventoryRepository : IRepository<Inventory>
    {
        public readonly RescueDogEntities db = RescueDog.Instance();


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