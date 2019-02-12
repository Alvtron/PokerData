using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library
{
    public static class RandomExtensions
    {
        public static void Shuffle<T>(this Random generator, T[] array)
        {
            int n = array.Length;
            while (n > 1)
            {
                int k = generator.Next(n--);
                T temp = array[n];
                array[n] = array[k];
                array[k] = temp;
            }
        }

        public static IEnumerable<T> Shuffle<T>(this Random generator, IEnumerable<T> source)
        {
            var temp = source.ToList();

            for (int i = 0; i < temp.Count; i++)
            {
                int j = generator.Next(i, temp.Count);
                yield return temp[j];

                temp[j] = temp[i];
            }
        }
    }
}
