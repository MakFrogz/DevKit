using System;
using System.Collections.Generic;
using System.Linq;

namespace Extensions
{
    public static class ListExtensions
    {
        private static Random _random;
        
        public static bool IsNullOrEmpty<T>(this IList<T> list)
        {
            return list == null || !list.Any();
        }
        
        public static List<T> Clone<T>(this IList<T> list)
        {
            var newList = new List<T>();
            foreach (var item in list)
            {
                newList.Add(item);
            }
            return newList;
        }
        
        public static IList<T> Shuffle<T>(this IList<T> list)
        {
            _random ??= new Random();
            var count = list.Count;
            while (count > 1)
            {
                --count;
                var index = _random.Next(count + 1);
                (list[index], list[count]) = (list[count], list[index]);
            }
            return list;
        }
        
        public static IList<T> Filter<T>(this IList<T> source, Predicate<T> predicate)
        {
            var list = new List<T>();
            foreach (var item in source)
            {
                if (predicate(item))
                {
                    list.Add(item);
                }
            }
            return list;
        }
    }
}