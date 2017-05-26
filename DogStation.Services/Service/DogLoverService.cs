using DogStation.Repository;
using DogStation.Entity.Models;
using DogStation.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;

namespace DogStation.Services
{
    public class DogLoverService
    {
        [Dependency]
        public DonateRepository donateDao { get; set; }
        [Dependency]
        public DogLoverRepository loverDao { get; set; }
        [Dependency]
        public InventoryRepository inventoryDao { get; set; }

        public List<Dictionary<string, object>> SeeDonations(long userId)
        {
            if (userId == 0) return null;
            return ConverterUtil.ConvertDonations(donateDao.GetDonations(userId));
        }

        public MyStatusCode Donate(List<DonateItem> items, long userId)
        {
            MyStatusCode state = MyStatusCode.Validated;
            if (userId == 0)
                state = MyStatusCode.Invalid;
            else
            {
                int lovesInc = CalLovesInc(items);
                DonateRecord record = new DonateRecord()
                {
                    lover = userId,
                    items = items.Count(),
                    lovesInc = lovesInc,
                    donateTime = DateTime.Now
                };
                
                using(var dbTransaction = loverDao.db.Database.BeginTransaction())
                {
                    try
                    {
                        long recordId = donateDao.Add(record).idDonateRecord;

                        foreach (DonateItem item in items)
                        {
                            item.record = recordId;
                        }
                        donateDao.Donate(items);

                        loverDao.IncLoves(userId, lovesInc);
                        dbTransaction.Commit();
                    }
                    catch (Exception)
                    {
                        dbTransaction.Rollback();
                        state = MyStatusCode.InternalServerError;
                    }
                }
            }
            return state;
        }

        private int CalLovesInc(List<DonateItem> items)
        {
            return 10 * items.Count();
        }
    }
}