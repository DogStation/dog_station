using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogStation.DAO.Interfaces
{
    public interface IRepository<T>
    {
        List<T> GetAll();
        T Get(int id);
        T Add(T t);
        bool Update(T t);
        T Delete(int id);
    }
}
