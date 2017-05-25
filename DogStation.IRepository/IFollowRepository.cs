using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DogStation.Entity.Models;

namespace DogStation.IRepository
{
    public interface IFollowRepository : IRepository<Follow>
    {
        List<Dog> GetFollowDogs(long userId);
        Follow Delete(long idDog, long idLover);
    }
}
