using DogStation.IRepository;
using DogStation.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DogStation.Entity.Models;
using Microsoft.Practices.Unity;
using DogStation.IServices;

namespace DogStation.Services
{
    public class DogService : IDogService
    {
        [Dependency]
        public IDogRepository dogDao { get; set; }
        [Dependency]
        public IDogLoverRepository loverDao { get; set; }
        [Dependency]
        public IFollowRepository followDao { get; set; }
        [Dependency]
        public IActivityRepository activityDao { get; set; }

        public List<Dictionary<String, Object>> GetAvailableDogs()
        {
            return ConverterUtil.ConvertDogs(dogDao.GetFreeDogs(), ConverterUtil.ConvertInfo.SenderOnly);
        }

        public Dictionary<string, object> SeeDogBasic(long idDog)
        {
            return ConverterUtil.ConvertDog(dogDao.Get(idDog), ConverterUtil.ConvertInfo.SenderAdopter);
        }

        public List<Dictionary<string, object>> SeeDogActivities(long idDog)
        {
            return ConverterUtil.ConvertActivities(activityDao.GetActivities(idDog));
        }

        public List<Dictionary<String, Object>> GetSentDogs(long userId)
        {
            if (userId == 0) return null;
            return ConverterUtil.ConvertDogs(dogDao.GetSentDogs(userId), ConverterUtil.ConvertInfo.SenderOnly);
        }

        public List<Dictionary<String, Object>> GetAdoptedDogs(long userId)
        {
            if (userId == 0) return null;
            return ConverterUtil.ConvertDogs(dogDao.GetAdoptedDogs(userId), ConverterUtil.ConvertInfo.SenderAdopter);
        }

        public List<Dictionary<String, Object>> GetFollowDogs(long userId)
        {
            if (userId == 0) return null;
            return ConverterUtil.ConvertDogs(followDao.GetFollowDogs(userId), ConverterUtil.ConvertInfo.SenderAdopter);
        }

        public long SendDog(Dog dog)
        {
            long idDog = 0;
            using (var dbTransaction = RescueDog.Transaction())
            {
                try
                {
                    idDog = dogDao.Add(dog).idDog;
                    loverDao.IncLoves(dog.sender, DefaultUtil.SentDogLovesPoint);
                    dbTransaction.Commit();
                }
                catch (Exception)
                {
                    dbTransaction.Rollback();
                    idDog = 0;
                }
            }
            return idDog;
        }

        public MyStatusCode AdoptDog(long idLover, long idDog)
        {
            MyStatusCode state = MyStatusCode.Validated;
            DogLover lover = loverDao.Get(idLover);
            Dog dog = dogDao.Get(idDog);
            if (dog == null || lover == null || dog.adopter != 0)
                state = MyStatusCode.Invalid;
            else if (lover.adoptDogs >= DefaultUtil.DefaultAdoptionMaximum)
                state = MyStatusCode.Limited;
            else if (lover.loves < dog.loves)
                state = MyStatusCode.CannotAfford;
            else
            {
                using (var dbTransaction = RescueDog.Transaction())
                {
                    try
                    {
                        dogDao.BeAdopted(dog, lover.idUser);
                        loverDao.AdoptDog(lover, dog.loves);
                        dbTransaction.Commit();
                    }
                    catch (Exception)
                    {
                        dbTransaction.Rollback();
                        state = MyStatusCode.InternalServerError;
                    }
                }
            }
            return state;
        }

        public MyStatusCode FollowDog(long idLover, long idDog)
        {
            MyStatusCode state = MyStatusCode.Validated;
            DogLover lover = loverDao.Get(idLover);
            Dog dog = dogDao.Get(idDog);
            if (lover == null || dog == null)
                state = MyStatusCode.Invalid;
            else if (lover.loveDogs >= DefaultUtil.DefaultFollowMaximum)
                state = MyStatusCode.Limited;
            else
            {
                using (var dbTransaction = RescueDog.Transaction())
                {
                    try
                    {
                        Follow follow = new Follow()
                        {
                            dog_ = idDog,
                            lover_ = idLover,
                            followTime = DateTime.Now
                        };
                        followDao.Add(follow);
                        loverDao.UpdateFollow(lover, 1);
                        dbTransaction.Commit();
                    }
                    catch (Exception)
                    {
                        dbTransaction.Rollback();
                        state = MyStatusCode.InternalServerError;
                    }
                }
            }
            return state;
        }

        public MyStatusCode UnfollowDog(long idLover, long idDog)
        {
            MyStatusCode state = MyStatusCode.Validated;
            DogLover lover = loverDao.Get(idLover);
            Dog dog = dogDao.Get(idDog);
            if (lover == null || dog == null)
                state = MyStatusCode.Invalid;
            else
            {
                using (var dbTransaction = RescueDog.Transaction())
                {
                    try
                    {
                        followDao.Delete(idDog, idLover);
                        loverDao.UpdateFollow(lover, -1);
                        dbTransaction.Commit();
                    }
                    catch (Exception)
                    {
                        dbTransaction.Rollback();
                        state = MyStatusCode.InternalServerError;
                    }
                }
            }
            return state;
        }
    }
}