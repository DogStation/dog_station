using DogStation.DAO;
using DogStation.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DogStation.Services
{
    public class DogService
    {
        private static readonly DogRepository dogDao = new DogRepository(new dog_stationEntities());

        public List<Dictionary<String, Object>> GetAvailableDogs()
        {
            return ConverterUtil.convertDogs(dogDao.GetAll());
        }

        public long AddDog(Dog dog)
        {
            return dogDao.Add(dog).idDog;
        }


    }
}