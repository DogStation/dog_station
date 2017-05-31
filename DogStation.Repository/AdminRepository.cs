using DogStation.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace DogStation.Repository
{
    public class AdminRepository : IRepository<Admin>
    {
        public readonly RescueDogEntities db = RescueDog.Instance();

        public Admin Add(Admin t)
        {
            db.Entry(t).State = EntityState.Added;
            db.SaveChangesAsync();
            return t;
        }

        public Admin Delete(long id)
        {
            throw new NotImplementedException();
        }

        public Admin Get(long id)
        {
            return db.Admin.Find(id);
        }

        public List<Admin> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<Admin> GetAll(long[] ids)
        {
            return db.Admin
                .Where(a => ids.Contains(a.idAdmin))
                .ToList();
        }

        public bool Update(Admin t)
        {
            throw new NotImplementedException();
        }
    }
}
