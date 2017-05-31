using System;
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

        public Inventory Get(string category, string name)
        {
            List<Inventory> list = db.Inventory
                .Where(i => i.category == category && i.name == name)
                .ToList();
            return list == null ? null : list[0];

        }

        public List<Inventory> GetAll()
        {
            return db.Inventory.ToList();
        }

        public bool Update(Inventory t)
        {
            db.Entry(t).Property(i => i.quantity).IsModified = true;
            db.SaveChangesAsync();
            return true;
        }

        public bool Update(List<DonateItem> items)
        {
            foreach(DonateItem item in items)
            {
                Inventory inventory = Get(item.category, item.name);
                if (inventory == null)
                    throw new Exception();
                inventory.quantity += item.number;
                Update(inventory);
            }
            return true;
        }
    }
}