using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DogStation.Utils;
using DogStation.Entity.Models;

namespace DogStation.IServices
{
    public interface IDogService
    {
        List<Dictionary<String, Object>> GetAvailableDogs();
        Dictionary<string, object> SeeDogBasic(long idDog);
        List<Dictionary<string, object>> SeeDogActivities(long idDog);
        List<Dictionary<String, Object>> GetSentDogs(long userId);
        List<Dictionary<String, Object>> GetAdoptedDogs(long userId);
        List<Dictionary<String, Object>> GetFollowDogs(long userId);
        long SendDog(Dog dog);
        MyStatusCode AdoptDog(long idLover, long idDog);
        MyStatusCode FollowDog(long idLover, long idDog);
        MyStatusCode UnfollowDog(long idLover, long idDog);

    }
}
