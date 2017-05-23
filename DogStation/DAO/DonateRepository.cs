using DogStation.DAO.Interfaces;
using DogStation.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DogStation.DAO
{
    public class DonateRepository : IRepository<DonateRecord>
    {
        private dog_stationEntities db = null;
        public DonateRepository(DbContext context)
        {
            db = (dog_stationEntities)context;
        }

        public DonateRecord Add(DonateRecord t)
        {
            db.Entry(t).State = EntityState.Added;
            db.SaveChanges();
            return t;
        }

        public void Donate(List<DonateItem> items)
        {
            foreach(DonateItem item in items)
            {
                db.Entry(item).State = EntityState.Added;
                db.SaveChangesAsync();
            }
        }

        public DonateRecord Get(long id)
        {
            return db.DonateRecord.Find(id);
        }

        public List<DonateRecord> GetDonations(long userId)
        {
            return db.DonateRecord
                .Where(d => d.lover == userId)
                .OrderByDescending(d => d.donateTime)
                .ToList();
        }

        public DonateRecord Delete(long id)
        {
            throw new NotImplementedException();
        }

        public List<DonateRecord> GetAll()
        {
            throw new NotImplementedException();
        }

        public bool Update(DonateRecord t)
        {
            throw new NotImplementedException();
        }
    }
}