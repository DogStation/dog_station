using DogStation.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DogStation.Utils
{
    public class ConverterUtil
    {
        public enum ConvertInfo
        {
            SenderOnly,
            AdopterOnly,
            SenderAdopter
        }

        public static List<Dictionary<String, Object>> ConvertDogs(List<Dog> dogs, ConvertInfo info)
        {
            List<Dictionary<String, Object>> result = new List<Dictionary<string, object>>();
            Dictionary<String, Object> dict = null;
            foreach (Dog dog in dogs)
            {
                dict = ConvertDog(dog, info);
                result.Add(dict);
            }
            return result;
        }

        public static Dictionary<String, Object> ConvertDog(Dog dog, ConvertInfo info)
        {
            Dictionary<String, Object> dict = new Dictionary<string, object>();
            if (dog == null)
                return dict;
            dict.Add("id", dog.idDog);
            dict.Add("name", dog.name);
            dict.Add("gender", dog.gender);
            dict.Add("kind", dog.kind);
            dict.Add("figure", dog.figure);
            dict.Add("sender", dog.sender);
            dict.Add("sendTime", dog.sendTime.ToShortDateString());
            if (info == ConvertInfo.SenderOnly || info == ConvertInfo.SenderAdopter)
            {
                dict.Add("senderName", dog.DogLover1.name);
                dict.Add("senderFigure", dog.DogLover1.figure);
            }
            if(info == ConvertInfo.AdopterOnly || info == ConvertInfo.SenderAdopter)
            {
                dict.Add("adopterName", dog.DogLover.name);
                dict.Add("adopterFigure", dog.DogLover.figure);
            }
            return dict;
        }

        public static List<Dictionary<string, object>> ConvertDonations(List<DonateRecord> records)
        {
            List<Dictionary<String, Object>> result = new List<Dictionary<string, object>>();
            Dictionary<string, object> dict;
            foreach(DonateRecord record in records)
            {
                dict = new Dictionary<string, object>();
                dict.Add("id", record.idDonateRecord);
                dict.Add("items", record.items.Value);
                dict.Add("lovesInc", record.lovesInc.Value);
                dict.Add("donateTime", record.donateTime);
                dict.Add("data", ConverterUtil.ConvertItems(record.DonateItem));
                result.Add(dict);
            }
            return result;

        }

        public static List<Dictionary<string, object>> ConvertItems(ICollection<DonateItem> items)
        {
            List<Dictionary<String, Object>> result = new List<Dictionary<string, object>>();
            Dictionary<string, object> dict;
            foreach (DonateItem item in items)
            {
                dict = new Dictionary<string, object>();
                dict.Add("id", item.idDonateItem);
                dict.Add("category", item.category);
                dict.Add("name", item.name);
                dict.Add("number", item.number);
                dict.Add("unit", item.unit);
                result.Add(dict);
            }
            return result;
        }

        public static List<Dictionary<string, object>> ConvertActivities(List<Activity> activities)
        {
            List<Dictionary<string, object>> result = new List<Dictionary<string, object>>();
            return result;
        }
    }
}