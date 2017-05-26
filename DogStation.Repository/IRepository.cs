using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogStation.Repository
{
    public interface IRepository<T>
    {
        List<T> GetAll();
        T Get(long id);
        T Add(T t);
        bool Update(T t);
        T Delete(long id);
    }
}
