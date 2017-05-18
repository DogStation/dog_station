using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DogStation.Utils
{
    public class ConverterUtil
    {
        public static List<Dictionary<String, Object>> convertDogs(List<Dog> dogs)
        {
            List<Dictionary<String, Object>> result = new List<Dictionary<string, object>>();
            Dictionary<String, Object> dict = null;
            foreach (Dog dog in dogs){
                dict = convertDog(dog);
                result.Add(dict);
            }
            return result;
        }

        public static Dictionary<String, Object> convertDog(Dog dog)
        {
            Dictionary<String, Object> dict = new Dictionary<string, object>();
            dict.Add("idDog", dog.idDog);
            dict.Add("name", dog.name);
            dict.Add("gender", dog.gender);
            dict.Add("kind", dog.kind);
            dict.Add("figure", dog.figure);
            dict.Add("sender", dog.sender);
            dict.Add("sendTime", dog.sendTime.Value.ToShortDateString());
            dict.Add("senderName", dog.DogLover.name);
            dict.Add("senderFigure", dog.DogLover.figure);
            return dict;
        }
    }
}