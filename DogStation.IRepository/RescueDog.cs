using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DogStation.Entity.Models;
using System.Data.Entity;
using System.Data;

namespace DogStation.IRepository
{
    public class RescueDog
    {
        private RescueDog()
        {

        }

        public static RescueDogEntities Instance()
        {
            return RescueDogNested.instance;
        }

        public static DbContextTransaction Transaction(IsolationLevel level = IsolationLevel.ReadCommitted)
        {
            return Instance().Database.BeginTransaction(level);
        }

        private static class RescueDogNested
        {
            public static readonly RescueDogEntities instance = new RescueDogEntities();
        }
    }
}
