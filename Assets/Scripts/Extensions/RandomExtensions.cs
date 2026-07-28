using System;
using System.Collections.Generic;

namespace Extensions
{
    public static class RandomExtensions
    {
        public static float NextFloat(this Random random)
        {
            return (float)random.NextDouble();
        }

        public static float NextFloat(this Random random, float min, float max)
        {
            return min + (max - min) * random.NextFloat();
        }
        
        public static bool NextBool(this Random random)
        {
            return random.Next(2) == 0;
        }
        
        public static bool NextBool(this Random random, float probability)
        {
            return random.NextFloat() < probability;
        }
        
        public static int NextSign(this Random random)
        {
            return random.NextBool() ? 1 : -1;
        }
        
        public static T PickRandom<T>(this Random random, IReadOnlyList<T> collection)
        {
            return collection.Count == 0 ? throw new InvalidOperationException("Collection is empty.") : collection[random.Next(collection.Count)];
        }
        
        public static T NextEnum<T>(this Random random) where T : Enum
        {
            var values = (T[])Enum.GetValues(typeof(T));
            return values[random.Next(values.Length)];
        }
    }
}