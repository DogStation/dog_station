using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DogStation.Entity.Models;

namespace DogStation.IRepository
{
    public interface IDogRepository : IRepository<Dog>
    {
        List<Dog> GetFreeDogs();
        List<Dog> GetSentDogs(long userId);
        List<Dog> GetAdoptedDogs(long userId);
        void BeAdopted(Dog dog, long userId);
    }
}
