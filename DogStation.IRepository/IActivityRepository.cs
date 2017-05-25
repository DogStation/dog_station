using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DogStation.Entity.Models;

namespace DogStation.IRepository
{
    public interface IActivityRepository : IRepository<Activity>
    {
        List<Activity> GetActivities(long idDog);
    }
}
