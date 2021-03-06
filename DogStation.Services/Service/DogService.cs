﻿using DogStation.Repository;
using DogStation.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DogStation.Entity.Models;
using Microsoft.Practices.Unity;

namespace DogStation.Services
{
    public class DogService
    {
        [Dependency]
        public DogRepository dogDao { get; set; }
        [Dependency]
        public DogLoverRepository loverDao { get; set; }
        [Dependency]
        public AdminRepository adminDao { get; set; }
        [Dependency]
        public FollowRepository followDao { get; set; }
        [Dependency]
        public ActivityRepository activityDao { get; set; }

        public List<Dictionary<String, Object>> GetAvailableDogs(long userId, int p)
        {
            return ConverterUtil.ConvertDogs(userId, dogDao.GetFreeDogs(p), ConverterUtil.ConvertInfo.SenderOnly);
        }

        public Dictionary<string, object> SeeDogBasic(long userId, long idDog)
        {
            return ConverterUtil.ConvertDog(userId, dogDao.Get(idDog), ConverterUtil.ConvertInfo.SenderAdopter);
        }

        public List<Dictionary<string, object>> SeeDogActivities(long idDog)
        {
            List<Activity> activities = activityDao.GetActivities(idDog);
            List<Dictionary<string, object>> result = ConverterUtil.ConvertActivities(activities);

            for(int i = 0; i< activities.Count(); ++i)
            {
                Activity a = activities.ElementAt(i);
                long[] idDogs = ConverterUtil.ConvertIds(a.dogs);
                long[] idLovers = ConverterUtil.ConvertIds(a.lovers);
                long[] idAdmins = ConverterUtil.ConvertIds(a.admins);

                result[i].Add("dogs", ConverterUtil.ConvertBasicDogs(dogDao.GetAll(idDogs)));
                result[i].Add("lovers", ConverterUtil.ConvertBasicLovers(loverDao.GetAll(idLovers)));
                result[i].Add("admins", ConverterUtil.ConvertBasicAdmins(adminDao.GetAll(idAdmins)));
            }

            return result;
        }

        public List<Dictionary<String, Object>> GetSentDogs(long userId)
        {
            if (userId == 0) return null;
            return ConverterUtil.ConvertDogs(userId, dogDao.GetSentDogs(userId), ConverterUtil.ConvertInfo.SenderOnly);
        }

        public List<Dictionary<String, Object>> GetAdoptedDogs(long userId)
        {
            if (userId == 0) return null;
            return ConverterUtil.ConvertDogs(0, dogDao.GetAdoptedDogs(userId), ConverterUtil.ConvertInfo.SenderAdopter);
        }

        public List<Dictionary<String, Object>> GetFollowDogs(long userId)
        {
            if (userId == 0) return null;
            return ConverterUtil.ConvertDogs(0, followDao.GetFollowDogs(userId), ConverterUtil.ConvertInfo.SenderAdopter);
        }

        public long SendDog(Dog dog)
        {
            long idDog = 0;
            using (var dbTransaction = dogDao.db.Database.BeginTransaction())
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
                using (var dbTransaction = dogDao.db.Database.BeginTransaction())
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
                using (var dbTransaction = dogDao.db.Database.BeginTransaction())
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
                using (var dbTransaction = dogDao.db.Database.BeginTransaction())
                {
                    try
                    {
                        if(followDao.Delete(idDog, idLover) != null)
                        {
                            loverDao.UpdateFollow(lover, -1);
                            dbTransaction.Commit();
                        }
                        else
                        {
                            state = MyStatusCode.Invalid;
                        }
                        
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