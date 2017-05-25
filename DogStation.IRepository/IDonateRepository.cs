using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DogStation.Entity.Models;


namespace DogStation.IRepository
{
    public interface IDonateRepository : IRepository<DonateRecord>
    {
        List<DonateRecord> GetDonations(long userId);
        void Donate(List<DonateItem> items);
    }
}
