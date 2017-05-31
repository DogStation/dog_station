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
            if (info == ConvertInfo.AdopterOnly || info == ConvertInfo.SenderAdopter)
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
            foreach (DonateRecord record in records)
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

        public static List<Dictionary<string, object>> ConvertComments(List<Comment> comments)
        {
            List<Dictionary<String, Object>> result = new List<Dictionary<string, object>>();
            Dictionary<String, Object> dict = null;
            foreach (Comment comment in comments)
            {
                dict = ConvertComment(comment);
                result.Add(dict);
            }
            return result;
        }

        public static Dictionary<string, object> ConvertComment(Comment comment)
        {
            Dictionary<String, Object> dict = new Dictionary<string, object>();
            if (comment == null)
                return dict;
            dict.Add("idComment", comment.idComment);
            dict.Add("lover", comment.commenter);
            dict.Add("loverFigure", comment.DogLover.figure);
            dict.Add("loverName", comment.DogLover.name);
            dict.Add("content", comment.content);
            dict.Add("commentTime", comment.commentTime);
            return dict;
        }

        public static List<Dictionary<string, object>> ConvertActivities(List<Activity> activities)
        {
            List<Dictionary<string, object>> result = new List<Dictionary<string, object>>();
            Dictionary<string, object> dict = null;
            foreach (Activity activity in activities)
            {
                dict = ConvertActivity(activity);
                result.Add(dict);
            }
            return result;
        }

        public static Dictionary<string, object> ConvertActivity(Activity a)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            if (a == null)
                return dict;
            string[] idDogs = a.dogs.Split(' ');
            string[] idAdmins = a.admins.Split(' ');
            string[] idLovers = a.lovers.Split(' ');
            string[] images = a.images.Split(' ');
            dict.Add("kind", a.kind);
            dict.Add("desc", a.desc);
            dict.Add("images", new List<string>(images));
            return dict;
        }

        public static List<Dictionary<string, object>> ConvertBasicDogs(List<Dog> dogs)
        {
            List<Dictionary<string, object>> result = new List<Dictionary<string, object>>();
            Dictionary<string, object> dict = null;
            foreach (Dog dog in dogs)
            {
                dict = new Dictionary<string, object>();
                dict.Add("id", dog.idDog);
                dict.Add("figure", dog.figure);
                dict.Add("name", dog.name);
                result.Add(dict);
            }
            return result;
        }

        public static List<Dictionary<string, object>> ConvertBasicLovers(List<DogLover> lovers)
        {
            List<Dictionary<string, object>> result = new List<Dictionary<string, object>>();
            Dictionary<string, object> dict = null;
            foreach (DogLover l in lovers)
            {
                dict = new Dictionary<string, object>();
                dict.Add("id", l.idUser);
                dict.Add("figure", l.figure);
                dict.Add("name", l.name);
                result.Add(dict);
            }
            return result;
        }

        public static List<Dictionary<string, object>> ConvertBasicAdmins(List<Admin> admins)
        {
            List<Dictionary<string, object>> result = new List<Dictionary<string, object>>();
            Dictionary<string, object> dict = null;
            foreach(Admin a in admins)
            {
                dict = new Dictionary<string, object>();
                dict.Add("id", a.idAdmin);
                dict.Add("figure", a.figure);
                dict.Add("name", a.name);
                result.Add(dict);
            }
            return result;
        }


        public static long[] ConvertIds(string ids)
        {
            string[] sids = ids.Trim().Split(' ');
            long[] lids = new long[sids.Length];
            for (int index = 0; index < sids.Length; ++index)
            {
                lids[index] = long.Parse(sids[index]);
            }
            return lids;
        }
    }
}