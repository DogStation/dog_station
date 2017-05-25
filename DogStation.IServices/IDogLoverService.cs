using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DogStation.Utils;
using DogStation.Entity.Models;

namespace DogStation.IServices
{
    public interface IDogLoverService
    {
        List<Dictionary<string, object>> SeeDonations(long userId);
        MyStatusCode Donate(List<DonateItem> items, long userId);

    }
}
