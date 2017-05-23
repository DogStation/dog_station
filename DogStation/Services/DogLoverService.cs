using DogStation.DAO;
using DogStation.Models;
using DogStation.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DogStation.Services
{
    public class DogLoverService
    {
        private static dog_stationEntities dbConnection = new dog_stationEntities();
        private static readonly DonateRepository donateDao = new DonateRepository(dbConnection);
        private static readonly DogLoverRepository loverDao = new DogLoverRepository(dbConnection);
        private static readonly InventoryRepository InventoryDao = new InventoryRepository(dbConnection);

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
                
                using(var dbTransaction = dbConnection.Database.BeginTransaction())
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