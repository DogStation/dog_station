using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DogStation.Entity.Models;

namespace DogStation.IRepository
{
    public interface IDogLoverRepository : IRepository<DogLover>
    {
        string GetPw(string username);
        long GetId(string username);
        void IncLoves(long loverId, int inc);
        void AdoptDog(DogLover lover, int dec);
        void UpdateFollow(DogLover lover, int c);
    }
}
